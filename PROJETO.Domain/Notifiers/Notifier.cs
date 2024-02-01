namespace PROJETO.Domain.Notifiers;

public class Notifier
{
    public string Key { get; private set; }

    public string Value { get; private set; }

    public Notifier(string key, string value)
    {
        Key = key;
        Value = value;
    }
}
