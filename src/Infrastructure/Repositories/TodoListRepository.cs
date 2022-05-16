using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class TodoListRepository : BaseRepository<TodoList>, ITodoListRepository
{
    private readonly ApplicationContext _dbContext;

    public TodoListRepository(ApplicationContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<TodoList> GetTodoListWithMoreItems(int count)
    {
        return _dbContext.TodoLists.OrderByDescending(d => d.Items).Take(count).ToList();
    }
}