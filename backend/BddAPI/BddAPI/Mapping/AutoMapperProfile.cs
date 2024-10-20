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

        CreateMap<User, UserResponseDto>();
        CreateMap<UserResponseDto, User>();

        CreateMap<CommunityHome, CommunityHomeRequestDto>();
        CreateMap<CommunityHomeRequestDto, CommunityHome>();

        CreateMap<CommunityHome, CommunityHomeResponseDto>();
        CreateMap<CommunityHomeResponseDto, CommunityHome>();

        CreateMap<Contract, ContractRequestDto>();
        CreateMap<ContractRequestDto, Contract>();

        CreateMap<Contract, ContractResponseDto>();
        CreateMap<ContractResponseDto, Contract>();

        CreateMap<ContractRangeRequestDto, ContractRange>();
        CreateMap<ContractRange, ContractRangeRequestDto>();

        CreateMap<Record, RecordRequestDto>();
        CreateMap<RecordRequestDto, Record>();

        CreateMap<Record, RecordResponseDto>();
        CreateMap<RecordResponseDto, Record>();

        CreateMap<ContractRange, ContractRangeResponseDto>();
        CreateMap<ContractRangeResponseDto, ContractRange>();
    }
}