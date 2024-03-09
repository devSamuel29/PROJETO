using Src.Domain.Response;
using Src.Domain.Request.Auth;

namespace Src.Domain.UseCases.Auth.Abstractions;

public interface ISignInUseCase
{
    Task<ResultResponse> Execute(SignInRequest request);
}
