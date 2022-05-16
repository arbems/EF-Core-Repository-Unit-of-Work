using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITodoListRepository : IRepositoryBase<TodoList>
    {
        IEnumerable<TodoList> GetTodoListWithMoreItems(int count);
    }
}