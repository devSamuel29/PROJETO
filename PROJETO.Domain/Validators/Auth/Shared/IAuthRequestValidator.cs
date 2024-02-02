using PROJETO.Domain.Identities;

namespace PROJETO.Domain.Validators.Auth.Shared;

public interface IAuthRequestValidator<Request>
{
    ValidationResult ValidateRequest(Request request);
}
