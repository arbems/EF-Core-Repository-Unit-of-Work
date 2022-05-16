using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommand : IRequest<int>
{
    public int ListId { get; set; }

    public string? Title { get; set; }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
{
    private readonly IRepositoryBase<TodoItem> _todoItemRepository;

    public CreateTodoItemCommandHandler(IRepositoryBase<TodoItem> todoItemRpository)
    {
        _todoItemRepository = todoItemRpository;
    }

    public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItem
        {
            ListId = request.ListId,
            Title = request.Title,
            Done = false
        };

        var newEntity = await _todoItemRepository.AddAsync(entity, cancellationToken);

        return newEntity.Id;
    }
}
