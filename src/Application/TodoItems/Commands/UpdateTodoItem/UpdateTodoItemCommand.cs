using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.TodoItems.Commands.UpdateTodoItem;

public class UpdateTodoItemCommand : IRequest
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IRepositoryBase<TodoItem> _todoItemRepository;
    private readonly IReadRepositoryBase<TodoItem> _todoItemReadRepository;

    public UpdateTodoItemCommandHandler(IRepositoryBase<TodoItem> todoItemRpository, IReadRepositoryBase<TodoItem> todoItemReadRepository)
    {
        _todoItemRepository = todoItemRpository;
        _todoItemReadRepository = todoItemReadRepository;
    }

    public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoItemReadRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        entity.Title = request.Title;
        entity.Done = request.Done;

        await _todoItemRepository.UpdateAsync(entity, cancellationToken);

        return Unit.Value;
    }
}
