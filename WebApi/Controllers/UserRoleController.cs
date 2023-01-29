using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]

public class UserRoleController : ControllerBase
{
    
    private readonly UserroleService _userroleService;

    public UserRoleController(UserroleService userroleService)
    {
        _userroleService = userroleService;
    }

    [HttpGet("GetuserRole")]
    public async Task<Response<List<UserRoleDto>>> GetUserRole()
    {
        return await _userroleService.GetUserRole();
    }

    [HttpPost("AddUserRole")]
    public async  Task<Response<UserRoleDto>> Add(UserRoleDto userRoleDto)
    {
        if (ModelState.IsValid)
        {
            return await _userroleService.AddUserRole(userRoleDto);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<UserRoleDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }



[HttpPut("Update")]
    public async Task<Response<UserRoleDto>> UpdateAddress(UserRoleDto userRoleDto)
    {       
        if (ModelState.IsValid)
        {
            return    await _userroleService.UpdateUserRole(userRoleDto); ;
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<UserRoleDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpDelete("Delete")]
    public async Task Delete(int id)
    {
        await _userroleService.DeleteUserRole(id);
    }
}