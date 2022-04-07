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
    private readonly IGenericRepository<TodoItem> _todoItemRpository;

    public DeleteTodoItemCommandHandler(IGenericRepository<TodoItem> todoItemRpository)
    {
        _todoItemRpository = todoItemRpository;
    }

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoItemRpository.GetById(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        await _todoItemRpository.DeleteAsync(entity, cancellationToken);

        return Unit.Value;
    }
}
