using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.MapperProfile;

public class InfrastructureProfile:Profile
{
    public InfrastructureProfile()
    {
        CreateMap<RolePremission, RolePremissionDto>();
        CreateMap<RolePremissionDto, RolePremission>();
        CreateMap<Premission, PremissionDto>();
        CreateMap<PremissionDto, Premission>();
        CreateMap<UserLogin, UserLoginDto>();
        CreateMap<UserLoginDto, UserLogin>();
        CreateMap<UserRole, UserRoleDto>();
        CreateMap<UserRoleDto, UserRole>();
        CreateMap<Role, RoleDto>();
        CreateMap<RoleDto, Role>();
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
       
    
     
    }
}