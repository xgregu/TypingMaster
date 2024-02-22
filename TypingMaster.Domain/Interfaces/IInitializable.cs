namespace TypingMaster.Domain.Interfaces;

public interface IInitializable
{
    public uint Priority { get; }
    Task Initialize();
}