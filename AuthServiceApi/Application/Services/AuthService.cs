using AutoMapper;
using AuthServiceApi.Application.Common.Exceptions;
using AuthServiceApi.Application.Common.Interfaces;
using AuthServiceApi.Application.Common.Models.User;
using AuthServiceApi.Application.Common.Utilities;
using AuthServiceApi.Domain.Constants;
using AuthServiceApi.Infrastructure.Interface;

namespace AuthServiceApi.Application.Services;
public class AuthService(IUnitOfWork unitOfWork,
                         IMapper mapper,
                         ITokenService tokenService,
                         ICurrentUser currentUser,
                         IUserRepository userRepository,
                         ICookieService cookieService
                         ) : IAuthService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICookieService _cookieService = cookieService;
    private readonly ITokenService _tokenService = tokenService;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IUserRepository _userRepository = userRepository;   
    public async Task<UserSignInResponse> SignIn(UserSignInRequest request)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.UserName == request.UserName)
                ?? throw UserException.BadRequestException(UserErrorMessage.UserNotExist);

            if (!StringHelper.Verify(request.Password, user.Password))
            {
                throw UserException.BadRequestException(UserErrorMessage.PasswordIncorrect);
            }

            var token = _tokenService.GenerateToken(user);
            _cookieService.Set(token);

            var response = _mapper.Map<UserSignInResponse>(user);
            response.Token = token;

            return response;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) if needed
            throw new Exception("An error occurred while signing in. Please try again.", ex);
        }
    }

    public async Task<UserSignUpResponse> SignUp(UserSignUpRequest request, CancellationToken token)
    {
        try
        {
            var isUserNameExist = await _unitOfWork.UserRepository.AnyAsync(x => x.UserName == request.UserName);
            if (isUserNameExist)
            {
                throw UserException.UserAlreadyExistsException(request.UserName);
            }

            var isEmailExist = await _unitOfWork.UserRepository.AnyAsync(x => x.Email == request.Email);
            if (isEmailExist)
            {
                throw UserException.UserAlreadyExistsException(request.Email);
            }

            var user = _mapper.Map<User>(request);
            user.Password = user.Password.Hash();           
            user.CreatedBy = request.Email;            
            await _unitOfWork.ExecuteTransactionAsync(async () => await _unitOfWork.UserRepository.AddAsync(user), token);

            var response = _mapper.Map<UserSignUpResponse>(user);
            return response;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) if needed
            throw new Exception("An error occurred while signing up. Please try again.", ex);
        }
    }

    public void Logout()
    {
        try
        {
            var token = _cookieService.Get();
            if (token == null)
            {
                throw new Exception("No active session found for logout.");
            }

            _cookieService.Delete();
        }
        catch (Exception ex)
        {
            // Log the exception (ex) if needed
            throw new Exception("An error occurred while logging out. Please try again.", ex);
        }
    }

    public async Task<UserProfileResponse> GetProfile()
    {
        try
        {
            var userId = _currentUser.GetCurrentUserId();
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User profile not found.");
            }

            var result = _mapper.Map<UserProfileResponse>(user);
            return result;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) if needed
            throw new Exception("An error occurred while retrieving the user profile. Please try again.", ex);
        }
    }
    public async Task<string> RefreshToken()
    {
        try
        {
            var userId = _currentUser.GetCurrentUserId();
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found. Unable to refresh token.");
            }

            var accessToken = _tokenService.GenerateToken(user);
            _cookieService.Set(accessToken);

            return accessToken;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) if needed
            throw new Exception("An error occurred while refreshing the token. Please try again.", ex);
        }
    }

}
