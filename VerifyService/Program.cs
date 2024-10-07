using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VerifyService.Exceptions;
using VerifyService.Models.Dtos;
using VerifyService.Options;
using VerifyService.Services;
using VerifyService.Services.Interfaces;
using VerifyService.Validator;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Load environment config
builder.Services.Configure<JwtSetting>(configuration.GetSection(JwtSetting.SECTION));
builder.Services.Configure<ApiSetting>(configuration.GetSection(ApiSetting.SECTION));

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

// Register Service
builder.Services.AddScoped<IUserServiceClient, UserServiceClient>();
builder.Services.AddScoped<IValidator<UserFormCreateRequestDto>, UserFormCreateValidator>();
builder.Services.AddScoped<IValidator<UserFormUpdateRequestDto>, UserFormUpdateValidator>();
builder.Services.AddScoped<IValidator<CourseFormRequestDto>, CourseFormValidator>();

// Register Custom Exception
builder.Services.AddControllers(options => { options.Filters.Add(new GlobalExceptionFilter()); });

builder.Services.AddHttpClient<UserServiceClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
