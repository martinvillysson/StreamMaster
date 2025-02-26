using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using StreamMaster.Domain.Configuration;
using StreamMaster.Domain.Dto;
using StreamMaster.Domain.Enums;
using StreamMaster.Domain.Models;
using StreamMaster.Domain.Services;
using StreamMaster.Streams.Channels;
using StreamMaster.Streams.Domain.Exceptions;
using StreamMaster.Streams.Domain.Interfaces;
using System.Collections.Concurrent;

namespace StreamMaster.Streams.UnitTests.Services;

public class ChannelServiceTests
{
    private class TestFixture
    {
        public Mock<ILogger<ChannelService>> LoggerMock { get; } = new();
        public Mock<ISwitchToNextStreamService> SwitchToNextStreamServiceMock { get; } = new();
        public Mock<ISourceBroadcasterService> SourceBroadcasterServiceMock { get; } = new();
        public Mock<IChannelBroadcasterService> ChannelBroadcasterServiceMock { get; } = new();
        public Mock<IStreamLimitsService> StreamLimitsServiceMock { get; } = new();
        public Mock<ICacheManager> CacheManagerMock { get; } = new();
        public Mock<IMessageService> MessageServiceMock { get; } = new();
        public ChannelService Service { get; }

        public TestFixture()
        {
            Service = new ChannelService(
                LoggerMock.Object,
                StreamLimitsServiceMock.Object,
                SourceBroadcasterServiceMock.Object,
                ChannelBroadcasterServiceMock.Object,
                CacheManagerMock.Object,
                MessageServiceMock.Object,
                SwitchToNextStreamServiceMock.Object,
                retryDelay: TimeSpan.Zero
            );
        }

        public Mock<IChannelBroadcaster> CreateMockChannelBroadcaster(
            int id = 1,
            string name = "TestChannel",
            bool isGlobal = false,
            SMChannelTypeEnum channelType = SMChannelTypeEnum.Regular)
        {
            var broadcaster = new Mock<IChannelBroadcaster>();
            broadcaster.Setup(x => x.Id).Returns(id);
            broadcaster.Setup(x => x.IsGlobal).Returns(isGlobal);

            var smChannelDto = new SMChannelDto
            {
                Id = id,
                Name = name,
                SMChannelType = channelType,
                SMStreamDtos = new List<SMStreamDto>(),
                SMChannelDtos = new List<SMChannelDto>(),
                StreamGroupIds = new List<int>(),
                StreamUrl = string.Empty,
                CurrentRank = -1,
                Rank = 0
            };

            broadcaster.Setup(x => x.SMChannel).Returns(smChannelDto);
            return broadcaster;
        }

        public Mock<ISourceBroadcaster> CreateMockSourceBroadcaster(
            string url = "http://test.com/stream",
            bool isFailed = false)
        {
            var broadcaster = new Mock<ISourceBroadcaster>();
            broadcaster.Setup(x => x.IsFailed).Returns(isFailed);
            return broadcaster;
        }

        public Mock<IClientConfiguration> CreateMockClientConfiguration(
            string uniqueId = "test-client",
            int channelId = 1,
            SMChannelTypeEnum channelType = SMChannelTypeEnum.Regular)
        {
            var config = new Mock<IClientConfiguration>();
            config.Setup(x => x.UniqueRequestId).Returns(uniqueId);

            var channel = new SMChannelDto
            {
                Id = channelId,
                Name = "TestChannel",
                SMChannelType = channelType,
                SMStreamDtos = new List<SMStreamDto>
            {
                new() { Id = "stream1", Url = "http://test.com/stream" }
            },
                SMChannelDtos = new List<SMChannelDto>(),
                StreamGroupIds = new List<int>(),
                StreamUrl = string.Empty,
                CurrentRank = -1,
                Rank = 0
            };

            config.Setup(x => x.SMChannel).Returns(channel);
            return config;
        }
    }

    [Fact]
    public async Task MoveToNextStream_ValidChannel_StopsSourceBroadcaster()
    {
        // Arrange
        var fixture = new TestFixture();
        var channelBroadcaster = fixture.CreateMockChannelBroadcaster();
        var streamInfo = new SMStreamInfo
        {
            Id = "test-stream",
            Name = "TestStream",
            Url = "http://test.com/stream",
            SMStreamType = SMStreamTypeEnum.Regular,
            CommandProfile = new CommandProfileDto()
        };
        channelBroadcaster.Setup(x => x.SMStreamInfo).Returns(streamInfo);

        var channelBroadcasters = new ConcurrentDictionary<int, IChannelBroadcaster>();
        channelBroadcasters.TryAdd(1, channelBroadcaster.Object);
        fixture.CacheManagerMock.Setup(x => x.ChannelBroadcasters)
            .Returns(channelBroadcasters);

        var sourceBroadcaster = fixture.CreateMockSourceBroadcaster();
        fixture.SourceBroadcasterServiceMock
            .Setup(x => x.GetStreamBroadcaster(streamInfo.Url))
            .Returns(sourceBroadcaster.Object);

        // Act
        await fixture.Service.MoveToNextStreamAsync(1);

        // Assert
        sourceBroadcaster.Verify(x => x.StopAsync(), Times.Once);
    }

    [Fact]
    public async Task AddClientToChannel_ValidConfiguration_AddsClientSuccessfully()
    {
        // Arrange
        var fixture = new TestFixture();
        var clientConfig = fixture.CreateMockClientConfiguration();
        var channelBroadcaster = fixture.CreateMockChannelBroadcaster();

        // Setup CacheManager's ChannelBroadcasters
        var channelBroadcasters = new ConcurrentDictionary<int, IChannelBroadcaster>();
        channelBroadcasters.TryAdd(clientConfig.Object.SMChannel.Id, channelBroadcaster.Object);
        fixture.CacheManagerMock.Setup(x => x.ChannelBroadcasters)
            .Returns(channelBroadcasters);

        // Setup stream limits service to allow the stream
        fixture.StreamLimitsServiceMock
            .Setup(x => x.GetStreamLimits(It.IsAny<string>()))
            .Returns((0, 100)); // currentCount, maxCount

        // Setup channel broadcaster service to return immediately
        fixture.ChannelBroadcasterServiceMock
            .Setup(x => x.GetOrCreateChannelBroadcasterAsync(
                It.IsAny<IClientConfiguration>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(channelBroadcaster.Object);

        // Setup switch to next stream service
        fixture.SwitchToNextStreamServiceMock
            .Setup(x => x.SetNextStreamAsync(
                It.IsAny<IChannelBroadcaster>(),
                It.IsAny<string>()))
            .ReturnsAsync(true);

        // Setup source broadcaster
        var sourceBroadcaster = fixture.CreateMockSourceBroadcaster();
        fixture.SourceBroadcasterServiceMock
            .Setup(x => x.GetOrCreateStreamBroadcasterAsync(
                It.IsAny<SMStreamInfo>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(sourceBroadcaster.Object);

        // Act
        bool result = await fixture.Service.AddClientToChannelAsync(clientConfig.Object, 1);

        // Assert
        result.ShouldBeTrue();
        channelBroadcaster.Verify(x => x.AddChannelStreamer(clientConfig.Object), Times.Once);
    }

    [Fact]
    public async Task GetOrCreateChannelBroadcaster_MultiViewChannel_SetupSuccessfully()
    {
        // Arrange
        var fixture = new TestFixture();
        var clientConfig = fixture.CreateMockClientConfiguration(
            channelType: SMChannelTypeEnum.MultiView);
        clientConfig.Object.SMChannel.SMChannelDtos.Add(new SMChannelDto());

        var channelBroadcaster = fixture.CreateMockChannelBroadcaster(
            channelType: SMChannelTypeEnum.MultiView);

        // Setup CacheManager's ChannelBroadcasters
        var channelBroadcasters = new ConcurrentDictionary<int, IChannelBroadcaster>();
        fixture.CacheManagerMock.Setup(x => x.ChannelBroadcasters)
            .Returns(channelBroadcasters);

        fixture.ChannelBroadcasterServiceMock
            .Setup(x => x.GetOrCreateChannelBroadcasterAsync(
                It.IsAny<IClientConfiguration>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(channelBroadcaster.Object);

        // Setup SourceBroadcasterService for MultiView
        fixture.SourceBroadcasterServiceMock
            .Setup(x => x.GetOrCreateMultiViewStreamBroadcasterAsync(
                It.IsAny<IChannelBroadcaster>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(fixture.CreateMockSourceBroadcaster().Object);

        // Act
        var result = await fixture.Service.GetOrCreateChannelBroadcasterAsync(
            clientConfig.Object, 1);

        // Assert
        result.ShouldNotBeNull();
        fixture.SourceBroadcasterServiceMock.Verify(
            x => x.GetOrCreateMultiViewStreamBroadcasterAsync(
                It.IsAny<IChannelBroadcaster>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task SwitchChannelToNextStream_Success_UpdatesChannelBroadcaster()
    {
        // Arrange
        var fixture = new TestFixture();
        var channelBroadcaster = fixture.CreateMockChannelBroadcaster();
        var streamInfo = new SMStreamInfo
        {
            Id = "test-stream",
            Name = "TestStream",
            Url = "http://test.com/stream",
            SMStreamType = SMStreamTypeEnum.Regular,
            CommandProfile = new CommandProfileDto()
        };

        fixture.SwitchToNextStreamServiceMock
            .Setup(x => x.SetNextStreamAsync(
                It.IsAny<IChannelBroadcaster>(),
                It.IsAny<string>()))
            .ReturnsAsync(true);

        channelBroadcaster.Setup(x => x.SMStreamInfo).Returns(streamInfo);

        var sourceBroadcaster = fixture.CreateMockSourceBroadcaster();
        fixture.SourceBroadcasterServiceMock
            .Setup(x => x.GetOrCreateStreamBroadcasterAsync(
                It.IsAny<SMStreamInfo>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(sourceBroadcaster.Object);

        // Act
        bool result = await fixture.Service.SwitchChannelToNextStreamAsync(
            channelBroadcaster.Object, null);

        // Assert
        result.ShouldBeTrue();
        sourceBroadcaster.Verify(
            x => x.AddChannelBroadcaster(channelBroadcaster.Object),
            Times.Once);
    }

    [Fact]
    public async Task SwitchChannelToNextStream_SourceBroadcasterAlwaysNull_ReturnsAfterMaxAttempts()
    {
        // Arrange
        var fixture = new TestFixture();
        var channelBroadcaster = fixture.CreateMockChannelBroadcaster();
        var clientConfig = fixture.CreateMockClientConfiguration();

        channelBroadcaster.Setup(x => x.FailoverInProgress).Returns(false);

        var streamInfo = new SMStreamInfo
        {
            Id = "test-stream",
            Name = "TestStream",
            Url = "http://test.com/stream",
            CommandProfile = new CommandProfileDto(),
            SMStreamType = SMStreamTypeEnum.Regular
        };
        channelBroadcaster.Setup(x => x.SMStreamInfo).Returns(streamInfo);

        fixture.SwitchToNextStreamServiceMock
            .Setup(x => x.SetNextStreamAsync(
                It.IsAny<IStreamStatus>(),
                It.IsAny<string>()))
            .ReturnsAsync(true);

        fixture.SourceBroadcasterServiceMock
            .Setup(x => x.GetOrCreateStreamBroadcasterAsync(
                It.IsAny<SMStreamInfo>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((ISourceBroadcaster?)null);

        // Act & Assert
        var exception = await Should.ThrowAsync<SourceBroadcasterNotFoundException>(async () =>
            await fixture.Service.SwitchChannelToNextStreamAsync(
                channelBroadcaster.Object,
                clientConfig.Object));

        exception.Message.ShouldBe("Failed to create source broadcaster");

        fixture.SourceBroadcasterServiceMock.Verify(
            x => x.GetOrCreateStreamBroadcasterAsync(
                It.IsAny<SMStreamInfo>(),
                It.IsAny<CancellationToken>()),
            Times.AtMost(4)); // Initial call, and 3 retries

        // Verify FailoverInProgress is reset
        channelBroadcaster.VerifySet(x => x.FailoverInProgress = false, Times.AtLeastOnce());
    }

    [Fact]
    public async Task SwitchChannelToNextStream_NoStreamsAvailable_ReturnsFalse()
    {
        // Arrange
        var fixture = new TestFixture();
        var channelBroadcaster = fixture.CreateMockChannelBroadcaster();
        var clientConfig = fixture.CreateMockClientConfiguration();

        // Setup empty stream list
        var smChannelDto = new SMChannelDto
        {
            Id = 1,
            Name = "TestChannel",
            SMStreamDtos = new List<SMStreamDto>(),
            SMChannelDtos = new List<SMChannelDto>(),
            StreamGroupIds = new List<int>(),
            CurrentRank = -1
        };
        channelBroadcaster.Setup(x => x.SMChannel).Returns(smChannelDto);
        channelBroadcaster.Setup(x => x.FailoverInProgress).Returns(false);

        // Setup SwitchToNextStreamService to return false (no streams available)
        fixture.SwitchToNextStreamServiceMock
            .Setup(x => x.SetNextStreamAsync(
                It.IsAny<IStreamStatus>(),
                It.IsAny<string>()))
            .ReturnsAsync(false);

        // Setup SMStreamInfo to be null after failed switch
        channelBroadcaster.Setup(x => x.SMStreamInfo).Returns((SMStreamInfo?)null);

        // Act
        bool result = await fixture.Service.SwitchChannelToNextStreamAsync(
            channelBroadcaster.Object,
            clientConfig.Object);

        // Assert
        result.ShouldBeFalse();
        fixture.SwitchToNextStreamServiceMock.Verify(
            x => x.SetNextStreamAsync(
                It.IsAny<IStreamStatus>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task SwitchChannelToNextStream_AllStreamsLimited_ReturnsFalse()
    {
        // Arrange
        var fixture = new TestFixture();
        var channelBroadcaster = fixture.CreateMockChannelBroadcaster();

        // Setup streams that are all limited
        var smChannelDto = new SMChannelDto
        {
            Id = 1,
            Name = "TestChannel",
            SMStreamDtos = new List<SMStreamDto>
            {
                new() { Id = "stream1", Url = "http://test.com/stream1", Rank = 0 },
                new() { Id = "stream2", Url = "http://test.com/stream2", Rank = 1 }
            },
            SMChannelDtos = new List<SMChannelDto>(),
            StreamGroupIds = new List<int>(),
            CurrentRank = 0
        };
        channelBroadcaster.Setup(x => x.SMChannel).Returns(smChannelDto);

        // Setup stream limits service to mark all streams as limited
        fixture.StreamLimitsServiceMock
            .Setup(x => x.IsLimited(It.IsAny<SMStreamDto>()))
            .Returns(true);

        fixture.SwitchToNextStreamServiceMock
            .Setup(x => x.SetNextStreamAsync(
                It.IsAny<IChannelBroadcaster>(),
                It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act
        bool result = await fixture.Service.SwitchChannelToNextStreamAsync(
            channelBroadcaster.Object, null);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task SwitchChannelToNextStream_WithOverrideStreamId_UsesSpecifiedStream()
    {
        // Arrange
        var fixture = new TestFixture();
        var channelBroadcaster = fixture.CreateMockChannelBroadcaster();
        var clientConfig = fixture.CreateMockClientConfiguration();
        const string overrideStreamId = "override-stream-1";

        // Setup the channel broadcaster
        channelBroadcaster.Setup(x => x.FailoverInProgress).Returns(false);

        fixture.SwitchToNextStreamServiceMock
            .Setup(x => x.SetNextStreamAsync(
                It.IsAny<IStreamStatus>(),
                overrideStreamId))
            .ReturnsAsync(true);

        // Setup stream info
        var streamInfo = new SMStreamInfo
        {
            Id = "test-stream",
            Name = "TestStream",
            Url = "http://test.com/stream",
            SMStreamType = SMStreamTypeEnum.Regular,
            CommandProfile = new CommandProfileDto()
        };
        channelBroadcaster.Setup(x => x.SMStreamInfo).Returns(streamInfo);

        // Setup source broadcaster
        var sourceBroadcaster = fixture.CreateMockSourceBroadcaster();
        fixture.SourceBroadcasterServiceMock
            .Setup(x => x.GetOrCreateStreamBroadcasterAsync(
                It.IsAny<SMStreamInfo>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(sourceBroadcaster.Object);

        // Act
        bool result = await fixture.Service.SwitchChannelToNextStreamAsync(
            channelBroadcaster.Object,
            clientConfig.Object,  // Added clientConfig parameter
            overrideStreamId);

        // Assert
        result.ShouldBeTrue();
        fixture.SwitchToNextStreamServiceMock.Verify(
            x => x.SetNextStreamAsync(
                It.IsAny<IStreamStatus>(),
                overrideStreamId),
            Times.Once);
        sourceBroadcaster.Verify(x => x.AddChannelBroadcaster(channelBroadcaster.Object), Times.Once);
    }

    [Fact]
    public async Task SwitchChannelToNextStream_AllStreamsCheckedOnce_DoesNotInfiniteLoop()
    {
        // Arrange
        var fixture = new TestFixture();
        var channelBroadcaster = fixture.CreateMockChannelBroadcaster();

        // Setup multiple streams
        var smChannelDto = new SMChannelDto
        {
            Id = 1,
            Name = "TestChannel",
            SMStreamDtos = new List<SMStreamDto>
        {
            new() { Id = "stream1", Url = "http://test.com/stream1", Rank = 0 },
            new() { Id = "stream2", Url = "http://test.com/stream2", Rank = 1 },
            new() { Id = "stream3", Url = "http://test.com/stream3", Rank = 2 }
        },
            SMChannelDtos = new List<SMChannelDto>(),
            StreamGroupIds = new List<int>(),
            CurrentRank = 1  // Start from middle stream
        };
        channelBroadcaster.Setup(x => x.SMChannel).Returns(smChannelDto);

        // Setup stream limits service to mark all streams as limited
        fixture.StreamLimitsServiceMock
            .Setup(x => x.IsLimited(It.IsAny<SMStreamDto>()))
            .Returns(true);

        fixture.SwitchToNextStreamServiceMock
            .Setup(x => x.SetNextStreamAsync(
                It.IsAny<IChannelBroadcaster>(),
                It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act
        bool result = await fixture.Service.SwitchChannelToNextStreamAsync(
            channelBroadcaster.Object, null);

        // Assert
        result.ShouldBeFalse();
        // Verify that SetNextStreamAsync was called exactly once
        fixture.SwitchToNextStreamServiceMock.Verify(
            x => x.SetNextStreamAsync(
                It.IsAny<IChannelBroadcaster>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public void Dispose_CleansUpResources()
    {
        // Arrange
        var fixture = new TestFixture();
        var channelBroadcaster = fixture.CreateMockChannelBroadcaster();
        var channelBroadcasters = new ConcurrentDictionary<int, IChannelBroadcaster>();
        channelBroadcasters.TryAdd(1, channelBroadcaster.Object);
        fixture.CacheManagerMock.Setup(x => x.ChannelBroadcasters)
            .Returns(channelBroadcasters);

        // Act
        fixture.Service.Dispose();

        // Assert
        channelBroadcaster.Verify(x => x.Stop(), Times.Once);
    }
}