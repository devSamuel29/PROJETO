namespace PROJETO.Domain.Exceptions.Auth.Shared;

public class InvalidBirthDateException : AuthException
{
    public InvalidBirthDateException()
        : base(
            "The user must be at least 18 years old or provide the minimum date 01/01/1900."
        ) { }
}
 