namespace TypingMaster.Domain;

public interface IInitializable
{
    public uint Priority { get; }
    Task Initialize();
}