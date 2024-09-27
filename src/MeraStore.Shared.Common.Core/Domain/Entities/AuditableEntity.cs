namespace MeraStore.Shared.Common.Core.Domain.Entities;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedDate { get; protected set; }
    public DateTime? ModifiedDate { get; protected set; }
    public string CreatedBy { get; protected set; } = null!;
    public string ModifiedBy { get; protected set; } = null!;

    protected AuditableEntity()
    {
        CreatedDate = DateTime.UtcNow;
    }
    protected AuditableEntity(string createdBy)
    {
        CreatedBy = createdBy;
        CreatedDate = DateTime.UtcNow;
    }

    public void SetModifiedInfo(string modifiedBy)
    {
        ModifiedBy = modifiedBy;
        ModifiedDate = DateTime.UtcNow;
    }
}