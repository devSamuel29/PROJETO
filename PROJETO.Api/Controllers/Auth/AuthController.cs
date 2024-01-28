using Microsoft.AspNetCore.Mvc;
using PROJETO.Domain.Identities;
using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;

namespace PROJETO.Api.Controllers.Auth;

[Route("v1/[controller]")]
[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly ISignInUseCase _signInUseCase;

    private readonly ISignUpUseCase _signUpUseCase;

    public AuthController(ISignInUseCase loginUseCase, ISignUpUseCase registerUseCase)
    {
        _signInUseCase = loginUseCase;
        _signUpUseCase = registerUseCase;
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInRequest signInRequest)
    {
        ResultIdentity result = await _signInUseCase.SignIn(signInRequest);

        return result.StatusCode switch
        {
            StatusCodeIdentity.SUCCESS => Ok(result.Data),
            StatusCodeIdentity.BAD_REQUEST => BadRequest(result.Data),
            _ => (IActionResult)StatusCode(StatusCodeIdentity.ERROR, result.Data),
        };
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
    {
        ResultIdentity result = await _signUpUseCase.SignUp(signUpRequest);

        return result.StatusCode switch
        {
            StatusCodeIdentity.CREATED => Created("localhost", result.Data),
            StatusCodeIdentity.BAD_REQUEST => BadRequest(result.Data),
            _ => StatusCode(StatusCodeIdentity.ERROR, result.Data),
        };
    }
}
