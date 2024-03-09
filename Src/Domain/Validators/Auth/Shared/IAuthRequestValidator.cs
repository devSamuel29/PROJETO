namespace Src.Domain.Validators.Auth.Shared;

public interface IAuthRequestValidator<Request>
{
    ValidationResult ValidateRequest(Request request);
}
