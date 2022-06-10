using EFCoreRepositoryUnitofWork.Entities;

namespace EFCoreRepositoryUnitofWork.Interfaces;

public interface ICustomPostRepository : IRepositoryBase<Post>, IReadRepositoryBase<Post>
{
    new Post Add(Post entity);

    Task<Post?> GetPostWithCommentsAsync(int id);

    IEnumerable<Post> GetPostWithMoreComments(int count);
}