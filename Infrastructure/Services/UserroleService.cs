using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserroleService
{


   private readonly DataContext _contex;
    private readonly IMapper _mapper;

    public UserroleService(DataContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
    }


    public async Task<Response<List<UserRoleDto>>> GetUserRole()
    {
        try
        {
            var result = await _contex.Premissions.ToListAsync();
            var mapped = _mapper.Map<List<UserRoleDto>>(result);
            return new Response<List<UserRoleDto>>(mapped);
        }
        catch (Exception ex)
        {
            return new Response<List<UserRoleDto>>(HttpStatusCode.InternalServerError,
                new List<string>() { ex.Message });
        }
    }

    public async Task<Response<UserRoleDto>> AddUserRole(UserRoleDto userRoleDto)
    {

        try
        {
            var existingUserDto = _contex.UserRoles.Where(x => x.UserRoleId == userRoleDto.UserRoleId).FirstOrDefault();
            if (existingUserDto != null)
            {
                return new Response<UserRoleDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "Customer with this Address already exists" });
            }

            var mapped = _mapper.Map <UserRole>(userRoleDto);
            await _contex.UserRoles.AddAsync(mapped);
            await _contex.SaveChangesAsync();
            userRoleDto.UserRoleId = mapped.UserRoleId;

            return new Response<UserRoleDto>(userRoleDto);
        }
        catch (Exception ex)
        {
            return new Response<UserRoleDto>(HttpStatusCode.InternalServerError, new List<string>(){ex.Message });
        }
    
}



public async  Task<Response<UserRoleDto>> UpdateUserRole(UserRoleDto userRoleDto)
    {
        try
        {
            var existinguser = _contex.UserRoles.Where(x => x.UserRoleId == userRoleDto.UserRoleId).AsNoTracking().FirstOrDefault();
            if (existinguser == null)
            {
                
                return new Response<UserRoleDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "We ca not updte it some thig is going wrong in " });     
            }
                         
            var mapped = _mapper.Map<UserRole>(userRoleDto);
            _contex.UserRoles.Update(mapped);
            await _contex.SaveChangesAsync();
            return new Response<UserRoleDto>(userRoleDto);
            
            var updated = _contex.SaveChangesAsync();
        }
        catch (Exception ex) 
        {
            return new Response<UserRoleDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }
    
    
    

    public async Task DeleteUserRole(int id)
    {
        var delete = await _contex.UserRoles.FirstAsync(x => x.UserRoleId == id);
        _contex.UserRoles.Remove(delete);
        await _contex.SaveChangesAsync();

    }
}