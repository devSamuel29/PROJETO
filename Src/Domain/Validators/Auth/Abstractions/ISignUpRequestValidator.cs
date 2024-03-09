using Src.Domain.Request.Auth;
using Src.Domain.Validators.Auth.Shared;

namespace Src.Domain.Validators.Auth.Abstractions;

public interface ISignUpRequestValidator : IAuthRequestValidator<SignUpRequest> { }
