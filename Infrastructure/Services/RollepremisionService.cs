using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class RollepremisionService
{
      private readonly DataContext _contex;
    private readonly IMapper _mapper;

    public RollepremisionService(DataContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
    }


    public async Task<Response<List<RolePremissionDto>>> GetRolePremission()
    {
        try
        {
            var result = await _contex.RolePremissions.ToListAsync();
            var mapped = _mapper.Map<List<RolePremissionDto>>(result);
            return new Response<List<RolePremissionDto>>(mapped);
        }
        catch (Exception ex)
        {
            return new Response<List<RolePremissionDto>>(HttpStatusCode.InternalServerError,
                new List<string>() { ex.Message });
        }
    }

    public async Task<Response<RolePremissionDto>> AddRolePremission(RolePremissionDto rolePremissionDto)
    {

        try
        {
            var existingUserDto = _contex.RolePremissions.Where(x => x.RolePremissionId == rolePremissionDto.RolePremissionId).FirstOrDefault();
            if (existingUserDto != null)
            {
                return new Response<RolePremissionDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "Customer with this Address already exists" });
            }

            var mapped = _mapper.Map <RolePremission>(rolePremissionDto);
            await _contex.RolePremissions.AddAsync(mapped);
            await _contex.SaveChangesAsync();
            rolePremissionDto.RolePremissionId = mapped.RolePremissionId;

            return new Response<RolePremissionDto>(rolePremissionDto);
        }
        catch (Exception ex)
        {
            return new Response<RolePremissionDto>(HttpStatusCode.InternalServerError, new List<string>(){ex.Message });
        }
    
}



public async  Task<Response<RolePremissionDto>> UpdateRolePremission(RolePremissionDto rolePremissionDto)
    {
        try
        {
            var existinguser = _contex.RolePremissions.Where(x => x.RolePremissionId == rolePremissionDto.RolePremissionId).AsNoTracking().FirstOrDefault();
            if (existinguser == null)
            {
                
                return new Response<RolePremissionDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "We ca not updte it some thig is going wrong in " });     
            }
                         
            var mapped = _mapper.Map<RolePremission>(rolePremissionDto);
            _contex.RolePremissions.Update(mapped);
            await _contex.SaveChangesAsync();
            return new Response<RolePremissionDto>(rolePremissionDto);
            
            var updated = _contex.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            
            return new Response<RolePremissionDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});

        }
    }
    
    
    

    public async Task DeleteRolePremission(int id)
    {
        var delete = await _contex.RolePremissions.FirstAsync(x => x.RolePremissionId == id);
        _contex.RolePremissions.Remove(delete);
        await _contex.SaveChangesAsync();

    }
}