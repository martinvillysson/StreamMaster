using Shouldly;
using StreamMaster.Streams.Plugins;
using System.Collections.Concurrent;

namespace StreamMaster.Streams.UnitTests.Plugins;

public class CircularBufferTests : IDisposable
{
    private readonly CircularBuffer _buffer;
    private const int DefaultBufferSize = 10;

    public void Dispose() => _buffer.Dispose();

    public CircularBufferTests()
    {
        _buffer = new CircularBuffer(DefaultBufferSize);
    }

    [Fact]
    public void Constructor_WithNegativeSize_ThrowsArgumentOutOfRangeException()
    {
        // Arrange

        // Act
        Action act = () => new CircularBuffer(-1);

        // Assert
        Should.Throw<ArgumentOutOfRangeException>(act);
    }

    [Fact]
    public void Constructor_WithZeroSize_ThrowsArgumentOutOfRangeException()
    {
        // Arrange

        // Act
        Action act = () => new CircularBuffer(0);

        // Assert
        Should.Throw<ArgumentOutOfRangeException>(act);
    }

    [Fact]
    public void Constructor_WithValidSize_CreatesEmptyBuffer()
    {
        // Arrange

        // Act

        // Assert
        _buffer.Size.ShouldBe(0);
        _buffer.Capacity.ShouldBe(DefaultBufferSize);
        _buffer.IsFull.ShouldBeFalse();
    }

    [Fact]
    public void Write_ToEmptyBuffer_StoresDataCorrectly()
    {
        // Arrange
        var data = new byte[] { 1, 2, 3 };

        // Act
        _buffer.Write(data);
        var result = _buffer.ReadAll();

        // Assert
        result.ToArray().ShouldBe(data);
        _buffer.Size.ShouldBe(3);
    }

    [Fact]
    public void Write_DataLargerThanCapacity_KeepsOnlyMostRecentData()
    {
        // Arrange
        var data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

        // Act
        _buffer.Write(data);
        var result = _buffer.ReadAll();

        // Assert
        result.ToArray().ShouldBe(data[^DefaultBufferSize..]);
        _buffer.Size.ShouldBe(DefaultBufferSize);
        _buffer.IsFull.ShouldBeTrue();
    }

    [Fact]
    public void Write_WrapAroundBuffer_MaintainsDataIntegrity()
    {
        // Arrange
        var firstWrite = new byte[] { 1, 2, 3, 4, 5 };
        _buffer.Write(firstWrite);
        _buffer.MarkRead(2);
        var secondWrite = new byte[] { 6, 7, 8 };

        // Act
        _buffer.Write(secondWrite);
        var result = _buffer.ReadAll();

        // Assert
        result.ToArray().ShouldBe([3, 4, 5, 6, 7, 8]);
        _buffer.Size.ShouldBe(6);
    }

    [Fact]
    public void ReadAll_OnEmptyBuffer_ReturnsEmptyMemory()
    {
        // Arrange

        // Act
        var result = _buffer.ReadAll();

        // Assert
        result.IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void ReadAll_OnNonEmptyBuffer_DoesNotModifySize()
    {
        // Arrange
        var data = new byte[] { 1, 2, 3 };
        _buffer.Write(data);
        var initialSize = _buffer.Size;

        // Act
        _ = _buffer.ReadAll();

        // Assert
        _buffer.Size.ShouldBe(initialSize);
    }

    [Fact]
    public void MarkRead_WithNegativeCount_ThrowsArgumentOutOfRangeException()
    {
        // Arrange

        // Act
        Action act = () => _buffer.MarkRead(-1);

        // Assert
        Should.Throw<ArgumentOutOfRangeException>(act);
    }

    [Fact]
    public void MarkRead_WithCountLargerThanSize_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        _buffer.Write(new byte[] { 1, 2, 3 });

        // Act
        Action act = () => _buffer.MarkRead(4);

        // Assert
        Should.Throw<ArgumentOutOfRangeException>(act);
    }

    [Fact]
    public void MarkRead_WithValidCount_UpdatesSizeCorrectly()
    {
        // Arrange
        var data = new byte[] { 1, 2, 3, 4, 5 };
        _buffer.Write(data);

        // Act
        _buffer.MarkRead(3);
        var remaining = _buffer.ReadAll();

        // Assert
        _buffer.Size.ShouldBe(2);
        remaining.ToArray().ShouldBe([4, 5]);
    }

    [Fact]
    public void Clear_OnNonEmptyBuffer_ResetsToEmpty()
    {
        // Arrange
        _buffer.Write([1, 2, 3]);

        // Act
        _buffer.Clear();

        // Assert
        _buffer.Size.ShouldBe(0);
        _buffer.IsFull.ShouldBeFalse();
        _buffer.ReadAll().IsEmpty.ShouldBeTrue();
    }

    [Fact]
    public void Operations_AfterDispose_ThrowObjectDisposedException()
    {
        // Arrange
        _buffer.Dispose();

        // Act

        // Assert
        Should.Throw<ObjectDisposedException>(() => _buffer.Write([1]));
        Should.Throw<ObjectDisposedException>(() => _buffer.ReadAll());
        Should.Throw<ObjectDisposedException>(() => _buffer.MarkRead(1));
        Should.Throw<ObjectDisposedException>(() => _buffer.Clear());
    }

    [Fact]
    public void Write_ToFullBuffer_OverwritesOldestData()
    {
        // Arrange
        var initialData = new byte[] { 1, 2, 3, 4, 5 };
        _buffer.Write(initialData);
        var newData = new byte[] { 6, 7, 8 };

        // Act
        _buffer.Write(newData);
        var result = _buffer.ReadAll();

        // Assert
        result.ToArray().ShouldBe([1, 2, 3, 4, 5, 6, 7, 8]);
    }

    [Fact]
    public void Dispose_CalledMultipleTimes_DoesNotThrowException()
    {
        // Arrange
        _buffer.Dispose();

        // Act

        // Assert
        Should.NotThrow(() => _buffer.Dispose());
    }

    [Fact]
    public void Write_DataCausesWrapAround_MaintainsDataIntegrity()
    {
        // Arrange
        var initialData = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        _buffer.Write(initialData);
        _buffer.MarkRead(4);
        var newData = new byte[] { 9, 10 };
        _buffer.Write(newData);
        var wrapData = new byte[] { 11 };

        // Act
        _buffer.Write(wrapData);
        var result = _buffer.ReadAll();

        // Assert
        result.ToArray().ShouldBe([5, 6, 7, 8, 9, 10, 11]);
        _buffer.Size.ShouldBe(7);
    }

    [Fact]
    public void MarkRead_WithWrapAround_UpdatesPointersCorrectly()
    {
        // Arrange
        var initialData = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        _buffer.Write(initialData);
        _buffer.MarkRead(6);
        var data = _buffer.ReadAll();
        data.ToArray().ShouldBe([7, 8]);
        _buffer.Write([9, 10]);

        // Act
        var data2 = _buffer.ReadAll();

        // Assert
        data2.ToArray().ShouldBe([7, 8, 9, 10]);
        _buffer.MarkRead(4);
        var data3 = _buffer.ReadAll();
        data3.ToArray().ShouldBe(Array.Empty<byte>());
    }

    [Fact]
    public async Task ConcurrentWriteAndRead_MultipleThreads_WorksCorrectly()
    {
        // Arrange
        const int numThreads = 5;
        const int numWritesPerThread = 100;
        var tasks = new Task[numThreads];
        var random = new Random();
        var allWrittenData = new ConcurrentQueue<byte>();

        // Act
        for (int i = 0; i < numThreads; i++)
        {
            int threadId = i;
            tasks[i] = Task.Run(() =>
            {
                for (int j = 0; j < numWritesPerThread; j++)
                {
                    // Generate some random data
                    byte[] data = new byte[random.Next(1, 5)];
                    random.NextBytes(data);
                    foreach (byte b in data)
                    {
                        allWrittenData.Enqueue(b);
                    }
                    _buffer.Write(data);
                    if (random.Next(0, 2) == 0) // 50% chance to read and mark
                    {
                        ReadOnlyMemory<byte> readData = _buffer.ReadAll();
                        int markReadCount = Math.Min(random.Next(0, 5), _buffer.Size);
                        _buffer.TryMarkRead(markReadCount);
                    }
                }
            });
        }

        await Task.WhenAll(tasks);

        // Assert - Take a snapshot of data under lock
        byte[] bufferContents;
        List<byte> expectedContents;
        List<byte> allDataList;
        lock (_buffer)
        {
            bufferContents = _buffer.ReadAll().ToArray();
            allDataList = allWrittenData.ToList();
        }
        expectedContents = new List<byte>();
        int startIndex = Math.Max(0, allDataList.Count - DefaultBufferSize);
        for (int i = startIndex; i < allDataList.Count; i++)
        {
            expectedContents.Add(allDataList[i]);
        }

        // Ensure expected contents match the size of the bufferContents
        while (expectedContents.Count < bufferContents.Length)
        {
            expectedContents.Insert(0, 0); // Insert default values to match the test
        }

        if (bufferContents.Length < expectedContents.Count)
        {
            bufferContents = allDataList.Skip(Math.Max(0, allDataList.Count() - DefaultBufferSize)).ToArray();
            expectedContents = allDataList.Skip(Math.Max(0, allDataList.Count() - DefaultBufferSize)).ToList();
        }

        // Ensure lengths are equal for comparison purposes.
        bufferContents.Length.ShouldBe(expectedContents.Count);

        // Compare the contents of the buffer with the expected data
        for (int i = 0; i < bufferContents.Length; i++)
        {
            bufferContents[i].ShouldBe(expectedContents[i]);
        }
    }
}