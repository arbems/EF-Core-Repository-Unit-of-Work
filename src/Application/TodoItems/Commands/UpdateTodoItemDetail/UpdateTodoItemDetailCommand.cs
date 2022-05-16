using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.TodoItems.Commands.UpdateTodoItemDetail;

public class UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public PriorityLevel Priority { get; set; }

    public string? Note { get; set; }
}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly IRepositoryBase<TodoItem> _todoItemRepository;
    private readonly IReadRepositoryBase<TodoItem> _todoItemReadRepository;

    public UpdateTodoItemDetailCommandHandler(IRepositoryBase<TodoItem> todoItemRpository, IReadRepositoryBase<TodoItem> todoItemReadRepository)
    {
        _todoItemRepository = todoItemRpository;
        _todoItemReadRepository = todoItemReadRepository;
    }

    public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoItemReadRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;

        await _todoItemRepository.UpdateAsync(entity, cancellationToken);

        return Unit.Value;
    }
}
