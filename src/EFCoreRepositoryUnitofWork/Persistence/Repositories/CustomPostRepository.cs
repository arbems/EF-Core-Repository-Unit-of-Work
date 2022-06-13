using EFCoreRepositoryUnitofWork.Entities;
using EFCoreRepositoryUnitofWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRepositoryUnitofWork.Persistence.Repositories;

public class CustomPostRepository : BaseRepository<Post>, ICustomPostRepository
{
    private readonly ApplicationContext _dbContext;

    public CustomPostRepository(ApplicationContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override Post Add(Post entity)
    {
        return base.Add(entity);
    }

    public async Task<Post?> GetPostWithCommentsAsync(int id)
    {
        IQueryable<Post> queryable = GetAll();
        return await queryable.Include(a => a.Comments).FirstOrDefaultAsync(a => a.Id == id);
    }

    public IEnumerable<Post> GetPostWithMoreComments(int count)
    {
        return _dbContext.Posts.OrderByDescending(d => d.Comments).Take(count).ToList();
    }
}