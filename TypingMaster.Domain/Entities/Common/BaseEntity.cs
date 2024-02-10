namespace TypingMaster.Domain.Entities.Common;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset LastChangeDate { get; set; }
}