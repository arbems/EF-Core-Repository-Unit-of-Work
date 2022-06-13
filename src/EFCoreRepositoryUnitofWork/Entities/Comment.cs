using EFCoreRepositoryUnitofWork.Common;

namespace EFCoreRepositoryUnitofWork.Entities;

public class Comment : Entity
{
    public int PostId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }
}
