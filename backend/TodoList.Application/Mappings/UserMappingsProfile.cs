using AutoMapper;
using TodoList.Application.Common.Responses.User;
using TodoList.Domain.Entities;

namespace TodoList.Application.Mappings;

public class UserMappingsProfile: Profile
{
    public UserMappingsProfile()
    {
        CreateMap<User, UserInfoResponse>()
            .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.Nickname))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserInfo.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.UserInfo.Surname))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.IsSuccessfully, opt => opt.MapFrom(src => true));
    }
}