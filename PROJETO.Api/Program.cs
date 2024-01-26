using System.Text;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using PROJETO.Infra.Database;
using PROJETO.Infra.Repositories.Auth;
using PROJETO.Domain.Repositories.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;
using PROJETO.Domain.UseCases.Auth.Implementations;
using PROJETO.Infra.DataSources.Abstractions.SqlServer.Auth;
using PROJETO.Infra.DataSources.Implementations;
using PROJETO.Infra.Services.Jwt.Abstractions;
using PROJETO.Infra.Services.Jwt.Implementations;
using PROJETO.Infra.Services.Ecrypters.Abstractions;
using PROJETO.Infra.Services.Ecrypters.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SqlServerContext>();

// USE CASES
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
builder.Services.AddScoped<IRegisterUseCase, RegisterUseCase>();

// SERVICES
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IEncrypterService, EncrypterService>();

// REPOSITORIES
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

// DATA SOURCES
builder.Services.AddScoped<IUserDataSource, UserDataSource>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    @"f@upa4!n#fv&OCfF$nU6pw&Temrl@iGcEMFX8lkd7ib7dTm^XzFHzit%VNLlHADlS*V7psJXYNxlSnv$9r!rXS!JwFngh6rCQLGSpLYXtUNw$BlG1rZJpFE1G@PwDiy5"
                )
            )
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
