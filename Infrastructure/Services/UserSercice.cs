using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserSercice
{

    private readonly DataContext _contex;
    private readonly IMapper _mapper;

    public UserSercice(DataContext contex, IMapper mapper)
    {
        _contex = contex;
        _mapper = mapper;
    }

    
    public async Task<Response<string>> Login(LoginDto model)
    {
        var existing = await _contex.Users
            .FirstOrDefaultAsync(x =>
                (x.email == model.FirstName || x.NumberPhone == model.FirstName) && x.Password == model.Password);

        if (existing == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest,
                new List<string>() { "Username or password is incorrect" });
        }

        return new Response<string>("You are welcome");
    }

    public async Task<Response<string>> Register(UserDto model)
    {
        var existing =
            await _contex.Users.FirstOrDefaultAsync(x => x.email == model.email || x.NumberPhone == model.NumberPhone);
        if (existing != null)
        {
            return new Response<string>(HttpStatusCode.BadRequest,
                new List<string>() { "This email or phone already exists" });
        }

        var mapped = _mapper.Map<User>(model);
        await _contex.Users.AddAsync(mapped);
        await _contex.SaveChangesAsync();
        return new Response<string>("You are successfully registered");

    }

    public async Task<Response<List<UserDto>>> GetUser()
    {
        try
        {
            var result = await _contex.Users.ToListAsync();
            var mapped = _mapper.Map<List<UserDto>>(result);
            return new Response<List<UserDto>>(mapped);
        }
        catch (Exception ex)
        {
            return  new Response<List<UserDto>>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }
    
    public async Task<Response<UserDto>> AddUser(UserDto userDto)
    {
        
        try
        {
            var existingUserDto = _contex.Users.Where(x => x.UserId == userDto.UserId).FirstOrDefault();
            if (existingUserDto != null)
            {
                return new Response<UserDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "Customer with this Address already exists" });
            }
            var mapped = _mapper.Map<User>(userDto);
            await _contex.Users.AddAsync(mapped);
            await _contex.SaveChangesAsync();
            userDto.UserId = mapped.UserId;

            return new Response<UserDto>(userDto);
        }
        catch (Exception ex)
        {
            return new Response<UserDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});
        }
    }


    
    
    public async  Task<Response<UserDto>> UpdateUser(UserDto userDto)
    {
        try
        {
            var existingStudent = _contex.Users.Where(x => x.UserId == userDto.UserId).AsNoTracking().FirstOrDefault();
            if (existingStudent != null)
            {
                return new Response<UserDto>(HttpStatusCode.BadRequest,
                    new List<string>() { "We ca not updte it some thig is going wrong in " });
            }

            var mapped = _mapper.Map<User>(userDto);
            _contex.Users.Update(mapped);
            await _contex.SaveChangesAsync();
            return new Response<UserDto>(userDto);
            
            var updated = _contex.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            
            return new Response<UserDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});

        }
    }

    public async Task DeleteUser(int id)
    {
        var delete = await _contex.Users.FirstAsync(x => x.UserId == id);
        _contex.Users.Remove(delete);
        await _contex.SaveChangesAsync();

    }
    
}