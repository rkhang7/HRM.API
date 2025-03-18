using AutoMapper;
using HRM.API.Domain.DTOs.Commom;
using HRM.API.Domain.DTOs.CreateCommomDto;
using HRM.API.Domain.DTOs.Role;
using HRM.API.Domain.DTOs.Users;
using HRM.API.Domain.Entities;

namespace HRM.API.Mappers
{
    public class MasterMapper : Profile
    {
        public MasterMapper()
        {
            CreateMap<CreateCommomDto, CommomEntity>();
            CreateMap<UpdateCommomDto, CommomEntity>();
            CreateMap<CreateRoleDTO, RoleEntity>();
            CreateMap<UpdateRoleDTO, RoleEntity>();
            CreateMap<CreateUserDTO, UserEntity>();
            CreateMap<UpdateUserDTO, UserEntity>();
        }
    }
}
