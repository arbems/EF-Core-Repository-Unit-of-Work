using EFCoreRepositoryUnitofWork.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRepositoryUnitofWork.Interfaces;

public interface IApplicationContext : IUnitOfWork
{
    DbSet<User> Users { get; }
    DbSet<Post> Posts { get; }
    DbSet<Comment> Comments { get; }
}