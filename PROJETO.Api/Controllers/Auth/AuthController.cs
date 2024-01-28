using Microsoft.AspNetCore.Mvc;
using PROJETO.Domain.Identities;
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
        ResultIdentity result = await _loginUseCase.SignIn(signInRequest);

        return result.StatusCode switch
        {
            StatusCodeIdentity.SUCCESS => Ok(await _loginUseCase.SignIn(signInRequest)),
            StatusCodeIdentity.BAD_REQUEST => BadRequest(result.Data),
            _ => (IActionResult)StatusCode(StatusCodeIdentity.ERROR, result.Data),
        };
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
    {
        ResultIdentity result = await _registerUseCase.SignUp(signUpRequest);

        return result.StatusCode switch
        {
            StatusCodeIdentity.CREATED => Created("localhost", result.Data),
            StatusCodeIdentity.BAD_REQUEST => BadRequest(result.Data),
            _ => StatusCode(StatusCodeIdentity.ERROR, result.Data),
        };
    }
}
