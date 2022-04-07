using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;

namespace Infrastructure.Repositories;

public class TodoListRepository : GenericRepository<TodoList>, ITodoListRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TodoListRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<TodoList> GetTodoListWithMoreItems(int count)
    {
        return _dbContext.TodoLists.OrderByDescending(d => d.Items).Take(count).ToList();
    }
}