using System.Text;

using Microsoft.OpenApi.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using PROJETO.Domain.Enuns.Role;
using PROJETO.Domain.Identities;
using PROJETO.Domain.Repositories.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;
using PROJETO.Domain.UseCases.Auth.Implementations;
using PROJETO.Domain.Validators.Auth.Abstractions;
using PROJETO.Domain.Validators.Implementations;

using PROJETO.Infra.Database;
using PROJETO.Infra.DataSources.SqlServer.User.Abstractions;
using PROJETO.Infra.DataSources.SqlServer.User.Implementations;
using PROJETO.Infra.Repositories.Auth;
using PROJETO.Infra.Services.Ecrypters.Abstractions;
using PROJETO.Infra.Services.Ecrypters.Implementations;
using PROJETO.Infra.Services.Jwt.Abstractions;
using PROJETO.Infra.Services.Jwt.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SqlServerContext>();

// VALIDATORS
builder.Services.AddScoped<ISignInRequestValidator, SignInRequestValidator>();
builder.Services.AddScoped<ISignUpRequestValidator, SignUpRequestValidator>();

// USE CASES
builder.Services.AddScoped<ISignInUseCase, SignInUseCase>();
builder.Services.AddScoped<ISignUpUseCase, SignUpUseCase>();

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

builder.Services.AddAuthorization(
    options =>
        options.AddPolicy(
            ERole.Admin.GetDisplayName(),
            p => p.RequireClaim(PolicyIdentiy.CLAIM_NAME, PolicyIdentiy.USER_CLAIM_NAME)
        )
);

builder.Services.AddAuthorization(
    options =>
        options.AddPolicy(
            ERole.User.GetDisplayName(),
            p => p.RequireClaim(PolicyIdentiy.CLAIM_NAME, PolicyIdentiy.ADMIN_CLAIM_NAME)
        )
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
