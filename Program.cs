using FreshCart.Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext for FreshCart database using MySQL (TiDB Cloud)
builder.Services.AddDbContext<FreshCartDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("FreshCartCS"), 
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("FreshCartCS"))
    ));

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Allow requests from this origin
               .AllowAnyHeader() // Allow any header
               .AllowAnyMethod(); // Allow any HTTP method
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI in development environment
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("MyPolicy"); // Apply CORS policy only in development
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
