using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]

public class RolePremissioncontroleer :ControllerBase
{
    private readonly RollepremisionService _rollepremisionService;

    public RolePremissioncontroleer(RollepremisionService rollepremisionService)
    {
        _rollepremisionService = rollepremisionService;
    }

    [HttpGet("GetRolePremission")]
    public async Task<Response<List<RolePremissionDto>>> GetRolePremission()
    {
        return await _rollepremisionService.GetRolePremission();
    }

    [HttpPost("AddRolePremission")]
    public async  Task<Response<RolePremissionDto>> Add(RolePremissionDto roleDto)
    {
        if (ModelState.IsValid)
        {
            return await _rollepremisionService.AddRolePremission(roleDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<RolePremissionDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }



    [HttpPut("UpdateRolePremission")]
    public async Task<Response<RolePremissionDto>> UpdateRolePremision(RolePremissionDto rolePremissionDto)
    {       
        if (ModelState.IsValid)
        {
            return    await _rollepremisionService.UpdateRolePremission(rolePremissionDto); ;
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<RolePremissionDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpDelete("DeleteRolePremission")]
    public async Task Delete(int id)
    {
        await _rollepremisionService.DeleteRolePremission(id);
    }
}