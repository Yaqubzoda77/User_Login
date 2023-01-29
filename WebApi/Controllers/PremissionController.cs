using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]

public class PremissionController :ControllerBase
{
       
    private readonly PremissionService _premissionService;

    public PremissionController(PremissionService premissionService)
    {
        _premissionService = premissionService;
    }

    [HttpGet("GetPremission")]
    public async Task<Response<List<PremissionDto>>> GetPremission()
    {
        return await _premissionService.GetPremission();
    }

    [HttpPost("AddUserRole")]
    public async  Task<Response<PremissionDto>> Add(PremissionDto premissionDto)
    {
        if (ModelState.IsValid)
        {
            return await _premissionService.AddPremission(premissionDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<PremissionDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }



    [HttpPut("Update")]
    public async Task<Response<PremissionDto>> UpdateAddress(PremissionDto premissionDto)
    {       
        if (ModelState.IsValid)
        {
            return    await _premissionService.UpdatePremission(premissionDto); ;
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<PremissionDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpDelete("Delete")]
    public async Task Delete(int id)
    {
        await _premissionService.DeletePremission(id);
    }
}