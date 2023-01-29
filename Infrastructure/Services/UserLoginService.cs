using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserLoginService
{


    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserLoginService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

}