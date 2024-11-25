using AutoMapper;
using AuthServiceApi.Application.Common.Models.User;

namespace AuthServiceApi.Application.Common.Mappings;

public class MapProfile : Profile
{
    public MapProfile()
    {        
        CreateMap<User, UserSignInRequest>().ReverseMap();
        CreateMap<User, UserSignInResponse>().ReverseMap();
        CreateMap<User, UserSignUpRequest>().ReverseMap();
        CreateMap<User, UserSignUpResponse>().ReverseMap();
        CreateMap<User, UserProfileResponse>().ReverseMap();
    }
}
