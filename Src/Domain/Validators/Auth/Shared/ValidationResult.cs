using Src.Domain.Notifiers;

namespace Src.Domain.Validators.Auth.Shared;

public class ValidationResult
{
    public bool HasNotification => Notifiers.Any();

    public IList<Notifier> Notifiers { get; } = [];
}
