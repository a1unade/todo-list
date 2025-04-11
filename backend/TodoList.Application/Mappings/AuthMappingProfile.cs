using AutoMapper;
using TodoList.Application.Common.Responses.Auth;
using TodoList.Application.Features.Authorization.Auth;
using TodoList.Domain.Entities;

namespace TodoList.Application.Mappings;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        // Маппинг для AuthCommand в User
        CreateMap<AuthCommand, User>()
            .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.Name + " " + src.Surname))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.UserInfo, opt => opt.MapFrom(src => new UserInfo
            {
                Name = src.Name,
                Surname = src.Surname,
                BirthDate = DateOnly.FromDateTime(src.DateOfBirth),
                Gender = src.Gender,
                Country = src.Country
            }));

        // Маппинг для User в AuthResponse
        CreateMap<User, AuthResponse>()
            .ForMember(dest => dest.Token, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.IsSuccessfully, opt => opt.MapFrom(src => true));
    }
}