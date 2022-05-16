using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.TodoLists.Commands.DeleteTodoList;

public class DeleteTodoListCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
{
    private readonly IRepositoryBase<TodoList> _todoListRepository;
    private readonly IReadRepositoryBase<TodoList> _todoListReadRepository;

    public DeleteTodoListCommandHandler(IRepositoryBase<TodoList> todoListRpository, IReadRepositoryBase<TodoList> todoListReadRepository)
    {
        _todoListRepository = todoListRpository;
        _todoListReadRepository = todoListReadRepository;
    }

    public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoListReadRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        }

        await _todoListRepository.DeleteAsync(entity, cancellationToken);

        return Unit.Value;
    }
}
