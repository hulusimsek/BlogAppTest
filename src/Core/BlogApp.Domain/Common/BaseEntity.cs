namespace BlogApp.Domain.Common;

/// <summary>
/// Base entity class for common properties
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? ModifiedDate { get; protected set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }

    public void SetModifiedDate()
    {
        ModifiedDate = DateTime.UtcNow;
    }
}