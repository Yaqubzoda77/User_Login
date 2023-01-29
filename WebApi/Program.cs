using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.MapperProfile;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(conf => conf.UseNpgsql(connection));
builder.Services.AddControllers();
//builder.Services.AddScoped<>();
builder.Services.AddScoped<UserroleService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<PremissionService>();
builder.Services.AddScoped<RolePremission>();
builder.Services.AddScoped<UserSercice>();
builder.Services.AddScoped<UserLoginService>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(InfrastructureProfile));




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
