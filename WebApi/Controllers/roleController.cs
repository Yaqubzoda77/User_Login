using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]

public class roleController :ControllerBase
{
    private readonly RoleService _roleService;

    public roleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("GetRole")]
    public async Task<Response<List<RoleDto>>> GetRole()
    {
        return await _roleService.GetRole();
    }

    [HttpPost("AddUserRole")]
    public async  Task<Response<RoleDto>> Add(RoleDto roleDto)
    {
        if (ModelState.IsValid)
        {
            return await _roleService.AddRole(roleDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<RoleDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }



    [HttpPut("Update")]
    public async Task<Response<RoleDto>> UpdateRole(RoleDto roleDto)
    {       
        if (ModelState.IsValid)
        {
            return    await _roleService.UpdateRole(roleDto); ;
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<RoleDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpDelete("Delete")]
    public async Task Delete(int id)
    {
        await _roleService.DeleteRole(id);
    }
}