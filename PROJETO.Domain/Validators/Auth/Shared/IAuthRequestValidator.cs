namespace PROJETO.Domain.Validators.Auth.Shared;

public interface IAuthRequestValidator<Request>
{
    void ValidateRequest(Request request);
}
