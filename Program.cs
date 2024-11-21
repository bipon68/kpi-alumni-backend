using DotNetEnv;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using KpiAlumni.Utils;
using KpiAlumni.Data;



// Application Builder
var builder = WebApplication.CreateBuilder(args);



// Load the .env file
Env.Load();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title= "KPI Alumni", Version="1.0.1" });
});

//Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

//--Connection String
var connectionString = EnvOperation.GetConnectionString();

//--Register MySQL Database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    try
    {
        //--For MySQL
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        options.EnableSensitiveDataLogging(true); // Enable sensitive data logging

    }
    catch
    {
        throw new Exception("Error: Unable to connect to the database. Please check the connection string");
    }
});

// --Configure the HTTP request pipeline.
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 200 * 1024 * 1024; // 200MB
});

//--Middleware
var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin"); // Apply the CORS policy to the application's request pipeline

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
