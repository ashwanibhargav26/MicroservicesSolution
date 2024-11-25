using AuthServiceApi.Application.Common.Models.User;

namespace AuthServiceApi.Application.Common.Interfaces;
public interface IAuthService
{
    Task<UserSignInResponse> SignIn(UserSignInRequest request);
    Task<UserSignUpResponse> SignUp(UserSignUpRequest request, CancellationToken token);
    void Logout();
    Task<string> RefreshToken();
    Task<UserProfileResponse> GetProfile();
}
