using Src.Domain.Response;
using Src.Domain.Request.Auth;

namespace Src.Domain.UseCases.Auth.Abstractions;

public interface ISignUpUseCase
{
    Task<ResultResponse> Execute(SignUpRequest request);
}
