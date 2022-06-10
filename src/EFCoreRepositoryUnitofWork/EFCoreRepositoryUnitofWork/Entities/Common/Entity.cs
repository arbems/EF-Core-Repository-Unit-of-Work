namespace EFCoreRepositoryUnitofWork.Common;

public abstract class Entity
{
    public int Id { get; set; }

    /* auditable fields */
    public DateTime? Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
