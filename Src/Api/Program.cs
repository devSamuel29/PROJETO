using System.Text;

using Microsoft.OpenApi.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Src.Domain.Enuns.Role;
using Src.Domain.Identities;
using Src.Domain.Repositories.Auth;
using Src.Domain.UseCases.Auth.Abstractions;
using Src.Domain.Validators.Implementations;
using Src.Domain.Validators.Auth.Abstractions;
using Src.Domain.UseCases.Auth.Implementations;

using Src.Infra.Database;
using Src.Infra.Repositories.Auth;
using Src.Infra.Services.Jwt.Abstractions;
using Src.Infra.Services.Jwt.Implementations;
using Src.Infra.Services.Ecrypters.Abstractions;
using Src.Infra.Services.Ecrypters.Implementations;
using Src.Infra.DataSources.SqlServer.User.Abstractions;
using Src.Infra.DataSources.SqlServer.User.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SqlServerContext>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("MyRedisConStr");
    options.InstanceName = "RedisContext";
});

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

builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy(
        ERole.Admin.GetDisplayName(),
        p => p.RequireClaim(PolicyIdentiy.CLAIM_NAME, PolicyIdentiy.USER_CLAIM_NAME)
    );

builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy(
        ERole.User.GetDisplayName(),
        p => p.RequireClaim(PolicyIdentiy.CLAIM_NAME, PolicyIdentiy.ADMIN_CLAIM_NAME)
    );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<SqlServerContext>();
await dbContext.Database.MigrateAsync();

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
