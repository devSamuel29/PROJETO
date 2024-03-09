using Microsoft.AspNetCore.Mvc;

using Src.Domain.Response;
using Src.Domain.Identities;
using Src.Domain.Request.Auth;
using Src.Domain.UseCases.Auth.Abstractions;

namespace Src.Api.Controllers.Auth;

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
        ResultResponse result = await _signInUseCase.Execute(signInRequest);

        return result.StatusCode switch
        {
            StatusCodeIdentity.SUCCESS => Ok(result),
            StatusCodeIdentity.BAD_REQUEST => BadRequest(result),
            _ => StatusCode(StatusCodeIdentity.ERROR, result),
        };
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
    {
        ResultResponse result = await _signUpUseCase.Execute(signUpRequest);
    
        return result.StatusCode switch
        {
            StatusCodeIdentity.CREATED => Created("localhost", result),
            StatusCodeIdentity.BAD_REQUEST => BadRequest(result),
            _ => StatusCode(StatusCodeIdentity.ERROR, result),
        };
    }
}
