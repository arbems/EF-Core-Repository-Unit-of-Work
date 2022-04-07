using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommand : IRequest<int>
{
    public string? Title { get; set; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly IGenericRepository<TodoList> _todoListRpository;

    public CreateTodoListCommandHandler(IGenericRepository<TodoList> todoListRpository)
    {
        _todoListRpository = todoListRpository;
    }

    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList();

        entity.Title = request.Title;

        var newEntity = await _todoListRpository.AddAsync(entity, cancellationToken);

        return newEntity.Id;
    }
}
