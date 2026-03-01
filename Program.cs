using Microsoft.EntityFrameworkCore;
using Apps_Apis;
using Apps_Apis.Data;
using Apps_Apis.Services.Implementaion;
using Apps_Apis.Services.Interfaces;
using Apps_Apis.UnitOfWork.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestingConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Apps Dashboard API", Version = "v1", Description = "API for managing applications and roles" });
});

builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

Console.WriteLine(builder.Configuration.GetConnectionString("TestingConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Apps Dashboard API v1");
});

app.UseHttpsRedirection();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
