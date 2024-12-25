using CodeClash.API.Extensions;
using CodeClash.API.Hubs;
using CodeClash.Application;
using CodeClash.Application.Services;
using CodeClash.Persistence;
using CodeClash.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;


services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
    
services.AddSignalR();
services.AddSingleton<IUserIdProvider, UserConnectionIdProvider>();

services.AddDbContext<ApplicationDbContext>();
//TODO Сделать, чтобы добавлялись только интерфесы с нужными методами. Интерфейсы реализовываются через Services, которые можно менять
services.AddScoped<PasswordHasher>();
services.AddApiAuthentication(configuration);

services.AddScoped<AuthService>();
services.AddScoped<TokenService>();
services.AddScoped<RoomService>();
services.AddScoped<IssueService>();
services.AddScoped<TestUserSolutionService>();
services.AddScoped<CompetitionService>();

services.AddScoped<UsersRepository>();
services.AddScoped<RoomsRepository>();
services.AddScoped<IssuesRepository>();

services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",policyBuilder =>
    {
        policyBuilder
            .WithOrigins("http://localhost:5173", "https://localhost:7282")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
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

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});

app.MapControllers();
app.MapHub<RoomHub>("/rooms"); // как сделать для разных комнат?

app.Run();
