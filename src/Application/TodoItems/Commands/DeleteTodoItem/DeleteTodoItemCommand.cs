using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;

using MediatR;

namespace Application.TodoItems.Commands.DeleteTodoItem;

public class DeleteTodoItemCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly IRepositoryBase<TodoItem> _todoItemRepository;
    private readonly IReadRepositoryBase<TodoItem> _todoItemReadRepository;

    public DeleteTodoItemCommandHandler(IRepositoryBase<TodoItem> todoItemRepository, IReadRepositoryBase<TodoItem> todoItemReadRepository)
    {
        _todoItemRepository = todoItemRepository;
        _todoItemReadRepository = todoItemReadRepository;
    }

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoItemReadRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        await _todoItemRepository.DeleteAsync(entity, cancellationToken);

        return Unit.Value;
    }
}
