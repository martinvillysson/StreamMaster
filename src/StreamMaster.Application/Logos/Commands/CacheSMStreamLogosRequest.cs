namespace StreamMaster.Application.Logos.Commands;

public class CacheSMStreamLogosRequest : IRequest<DataResponse<bool>>;

[LogExecutionTimeAspect]
public class CacheSMStreamLogosRequestHandler(ILogoService logoService) : IRequestHandler<CacheSMStreamLogosRequest, DataResponse<bool>>
{
    public async Task<DataResponse<bool>> Handle(CacheSMStreamLogosRequest command, CancellationToken cancellationToken)
    {
        await logoService.CacheSMStreamLogosAsync(cancellationToken);
        return DataResponse.True;
    }
}
