using CodeClash.API.Extensions;
using CodeClash.API.Hubs;
using CodeClash.API.Services;
using CodeClash.Application;
using CodeClash.Core.Services;
using CodeClash.Persistence;
using CodeClash.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// builder.WebHost.UseUrls("http://0.0.0.0:5099"); // пока так

services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

builder.Services.AddSignalR();

services.AddDbContext<ApplicationDbContext>();
//TODO Сделать, чтобы добавлялись только интерфесы с нужными методами. Интерфейсы реализовываются через Services, которые можно менять
services.AddScoped<PasswordHasher>();
services.AddApiAuthentication(configuration);

services.AddScoped<AuthService>();
services.AddScoped<TokenService>();
services.AddScoped<RoomService>();
services.AddScoped<IssueService>();

services.AddScoped<UsersRepository>();
services.AddScoped<RoomsRepository>();
services.AddScoped<IssuesRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.MapControllers();
app.MapHub<RoomHub>("/room"); // как сделать для разных комнат?

app.Run();
