using CourseService.Exceptions;
using CourseService.Models;
using CourseService.Models.Database;
using CourseService.Options;
using CourseService.Repositories;
using CourseService.Repositories.Interfaces;
using CourseService.Services;
using CourseService.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Load environment config
builder.Services.Configure<JwtSetting>(configuration.GetSection(JwtSetting.SECTION));
builder.Services.Configure<ApiSetting>(configuration.GetSection(ApiSetting.SECTION));

// Database config
builder.Services.AddDbContext<DatabaseContext>
    (options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// JWT Auth Config
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
         ValidAudience = builder.Configuration["JwtConfig:Audience"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"])),
     };
 });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register Service
builder.Services.AddScoped<ICourseService, CourseService.Services.CourseService>();

// Register Repository
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

// Register Custom Exception
builder.Services.AddControllers(options => { options.Filters.Add(new GlobalExceptionFilter()); });

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
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

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
