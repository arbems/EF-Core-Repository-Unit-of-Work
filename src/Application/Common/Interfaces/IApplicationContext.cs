using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Interfaces;

public interface IApplicationContext : IUnitOfWork
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }

    IDbContextTransaction GetCurrentTransaction();
    bool HasActiveTransaction { get; }

    Task<IDbContextTransaction> BeginTransactionAsync();

    Task CommitTransactionAsync(IDbContextTransaction transaction);

    void RollbackTransaction();
}