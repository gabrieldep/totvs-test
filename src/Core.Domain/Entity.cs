namespace Core.Domain;
public abstract class Entity
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; private set; } = null;

    public void MarkAsDeleted()
    {
        DeletedAt = DateTime.UtcNow;
    }
}
