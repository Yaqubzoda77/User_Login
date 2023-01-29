using System.Net;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]

public class UserController :ControllerBase
{
       
    private readonly UserSercice _userSercice;

    public UserController(UserSercice userSercice)
    {
        _userSercice = userSercice;
    }

    [HttpGet("Getuser")]
    public async Task<Response<List<UserDto>>> GetUser()
    {
        return await _userSercice.GetUser();
    }
    
    [HttpPost("AddUser")]
    public async  Task<Response<UserDto>> Add(UserDto userDto)
    {
        if (ModelState.IsValid)
        {
            return await _userSercice.AddUser(userDto);
        }
        else
        {
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<UserDto>(HttpStatusCode.BadRequest, errors);
        }
       
    }
        



    [HttpPut("Update")]
    public async Task<Response<UserDto>> Updateuser(UserDto userDto)
    {       
        if (ModelState.IsValid)
        {
            return   await _userSercice.UpdateUser(userDto); ;
        }
        else
        {         
            
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<UserDto>(HttpStatusCode.BadRequest, errors); 
        }
    }

    [HttpDelete("Delete")]
    public async Task Delete(int id)
    {
        await _userSercice.DeleteUser(id);
    }
    
    
    [HttpPost("login")]
    public async Task<Response<string>> Login(LoginDto model)
    {
        if (ModelState.IsValid)
        {
            return await _userSercice.Login(model);
        }
        else
        {
          
           
            return new Response<string>(HttpStatusCode.BadRequest, GetModelErrors());
        }
    }
    
    [HttpPost("register")]
    public async Task<Response<string>> Register(UserDto model)
    {
        if (ModelState.IsValid)
        {
            return await _userSercice.Register(model);
        }
        else
        {
            return new Response<string>(HttpStatusCode.BadRequest, GetModelErrors());
        }
        
    }
    
    [NonAction]
    private List<string> GetModelErrors()
    {
        var errors = ModelState.Values.SelectMany(x => x.Errors.Select(x=>x.ErrorMessage)).ToList();
        return errors;
    }
}