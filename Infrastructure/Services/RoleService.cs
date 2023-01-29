using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class RoleService
{
     private readonly DataContext _contex;
    private readonly IMapper _mapper;

    public RoleService(DataContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
    }


    public async Task<Response<List<RoleDto>>> GetRole()
    {
        try
        {
            var result = await _contex.Premissions.ToListAsync();
            var mapped = _mapper.Map<List<RoleDto>>(result);
            return new Response<List<RoleDto>>(mapped);
        }
        catch (Exception ex)
        {
            return new Response<List<RoleDto>>(HttpStatusCode.InternalServerError,
                new List<string>() { ex.Message });
        }
    }

    public async Task<Response<RoleDto>> AddRole(RoleDto roleDto)
    {

        try
        {
            var existingUserDto = _contex.Roles.Where(x => x.RoleId == roleDto.RoleId).FirstOrDefault();
            if (existingUserDto != null)
            {
                return new Response<RoleDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "Customer with this Address already exists" });
            }

            var mapped = _mapper.Map <Role>(roleDto);
            await _contex.Roles.AddAsync(mapped);
            await _contex.SaveChangesAsync();
            roleDto.RoleId = mapped.RoleId;
            return new Response<RoleDto>(roleDto);
        }
        catch (Exception ex)
        {
            return new Response<RoleDto>(HttpStatusCode.InternalServerError, new List<string>(){ex.Message });
        }
    
}



public async  Task<Response<RoleDto>> UpdateRole(RoleDto roleDto)
    {
        try
        {
            var existinguser = _contex.Roles.Where(x => x.RoleId == roleDto.RoleId).AsNoTracking().FirstOrDefault();
            if (existinguser == null)
            {
                
                return new Response<RoleDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "We ca not updte it some thig is going wrong in " });     
            }
                         
            var mapped = _mapper.Map<Role>(roleDto);
            _contex.Roles.Update(mapped);
            await _contex.SaveChangesAsync();
            return new Response<RoleDto>(roleDto);
            
            var updated = _contex.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            
            return new Response<RoleDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});

        }
    }
    
    
    

    public async Task DeleteRole(int id)
    {
        var delete = await _contex.Roles.FirstAsync(x => x.RoleId == id);
        _contex.Roles.Remove(delete);
        await _contex.SaveChangesAsync();

    }
}