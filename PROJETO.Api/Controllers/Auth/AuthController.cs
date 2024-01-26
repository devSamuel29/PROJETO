using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;

namespace PROJETO.Api.Controllers.Auth;

[Route("v1/[controller]")]
[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;

    private readonly IRegisterUseCase _registerUseCase;

    public AuthController(ILoginUseCase loginUseCase, IRegisterUseCase registerUseCase)
    {
        _loginUseCase = loginUseCase;
        _registerUseCase = registerUseCase;
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInRequest signInRequest)
    {
        return Ok(await _loginUseCase.SignIn(signInRequest));
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpRequest signUpRequest) 
    { 
        return Ok(await _registerUseCase.SignUp(request: signUpRequest));
    }
}
