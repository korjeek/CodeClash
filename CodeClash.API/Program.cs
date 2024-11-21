using CodeClash.API.Services;
using CodeClash.Application;
using CodeClash.Core.Services;
using CodeClash.Persistence;
using CodeClash.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



 builder.Services.AddDbContext<ApplicationDbContext>(
     options => options.UseNpgsql(configuration.GetConnectionString(nameof(ApplicationDbContext))));


// builder.Services.AddDbContext<ApplicationDbContext>(
//     options => options.UseSqlite("Data Source=./Database/TestDB.db")
// );


builder.Services.AddScoped<PasswordHasher>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<IssueService>();

builder.Services.AddScoped<IssuesRepository>();
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<RoomsRepository>();
builder.Services.AddScoped<IssuesRepository>();

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

app.MapControllers();

app.Run();
