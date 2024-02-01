using PROJETO.Domain.Notifiers;

namespace PROJETO.Domain.Identities;

public class ValidationResult
{
    public bool HasNotification => Notifiers.Any();

    public IList<Notifier> Notifiers { get; } = new List<Notifier>();
}
