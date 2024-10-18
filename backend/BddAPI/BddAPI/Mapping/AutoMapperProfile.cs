using AutoMapper;
using BddAPI.DTOs.Request;
using BddAPI.DTOs.Response;
using BddAPI.Models;

namespace BddAPI.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserRequestDto>();
        CreateMap<UserRequestDto, User>();

        CreateMap<CommunityHome, CommunityHomeRequestDto>();
        CreateMap<CommunityHomeRequestDto, CommunityHome>();

        CreateMap<CommunityHome, CommunityHomeResponseDto>();
        CreateMap<CommunityHomeResponseDto, CommunityHome>();
    }
}