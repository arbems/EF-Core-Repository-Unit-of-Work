using EFCoreRepositoryUnitofWork.Common;

namespace EFCoreRepositoryUnitofWork.Entities;

public class Post : Entity
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    private List<Comment> _comments = new();
    public ICollection<Comment> Comments => _comments;
}
