using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class PremissionService
{

    private readonly DataContext _contex;
    private readonly IMapper _mapper;

    public PremissionService(DataContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
    }


    public async Task<Response<List<PremissionDto>>> GetPremission()
    {
        try
        {
            var result = await _contex.Premissions.ToListAsync();
            var mapped = _mapper.Map<List<PremissionDto>>(result);
            return new Response<List<PremissionDto>>(mapped);
        }
        catch (Exception ex)
        {
            return new Response<List<PremissionDto>>(HttpStatusCode.InternalServerError,
                new List<string>() { ex.Message });
        }
    }

    public async Task<Response<PremissionDto>> AddPremission(PremissionDto premissionDto)
    {

        try
        {
            var existingUserDto = _contex.Premissions.Where(x => x.PremissionId == premissionDto.PremissionId).FirstOrDefault();
            if (existingUserDto != null)
            {
                return new Response<PremissionDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "Customer with this Address already exists" });
            }

            var mapped = _mapper.Map < Premission>(premissionDto);
            await _contex.Premissions.AddAsync(mapped);
            await _contex.SaveChangesAsync();
            premissionDto.PremissionId = mapped.PremissionId;
            return new Response<PremissionDto>(premissionDto);
        }
        catch (Exception ex)
        {
            return new Response<PremissionDto>(HttpStatusCode.InternalServerError, new List<string>(){ex.Message });
        }
    
}



public async  Task<Response<PremissionDto>> UpdatePremission(PremissionDto premissionDto)
    {
        try
        {
            var existinguser = _contex.Premissions.Where(x => x.PremissionId == premissionDto.PremissionId).AsNoTracking().FirstOrDefault();
            if (existinguser == null)
            {
                
                return new Response<PremissionDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "We ca not updte it some thig is going wrong in " });     
            }
                         
            var mapped = _mapper.Map<Premission>(premissionDto);
            _contex.Premissions.Update(mapped);
            await _contex.SaveChangesAsync();
            return new Response<PremissionDto>(premissionDto);
            
            var updated = _contex.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            
            return new Response<PremissionDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});

        }
    }
    
    
    

    public async Task DeletePremission(int id)
    {
        var delete = await _contex.Premissions.FirstAsync(x => x.PremissionId == id);
        _contex.Premissions.Remove(delete);
        await _contex.SaveChangesAsync();

    }
}