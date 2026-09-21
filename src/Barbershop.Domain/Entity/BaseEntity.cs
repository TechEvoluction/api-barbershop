namespace Barbershop.Domain.Entity;

public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; protected set; }

    public BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}