using PROJETO.Domain.Request.Auth;
using PROJETO.Domain.Validators.Auth.Shared;

namespace PROJETO.Domain.Validators.Auth.Abstractions;

public interface ISignUpRequestValidator : IAuthRequestValidator<SignUpRequest> { }
