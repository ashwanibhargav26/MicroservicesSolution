using AuthServiceApi.Domain.Constants;
using AuthServiceApi.Domain.Enums;

namespace AuthServiceApi.Application.Common.Exceptions;

public static class ProgramException
{
    public static UserFriendlyException AppsettingNotSetException()
        => new(ErrorCode.Internal, ErrorMessage.AppConfigurationMessage, ErrorMessage.Internal);
}
