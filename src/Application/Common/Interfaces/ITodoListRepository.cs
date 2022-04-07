using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITodoListRepository : IGenericRepository<TodoList>
    {
        IEnumerable<TodoList> GetTodoListWithMoreItems(int count);
    }
}