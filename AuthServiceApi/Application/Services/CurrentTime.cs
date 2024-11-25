using AuthServiceApi.Application.Common.Interfaces;

namespace AuthServiceApi.Application.Services;

public class CurrentTime : ICurrentTime
{
    public DateTime GetCurrentTime() => DateTime.UtcNow;
}
