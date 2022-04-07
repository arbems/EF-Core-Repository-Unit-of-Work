using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.TodoLists.Commands.UpdateTodoList;

public class UpdateTodoListCommand : IRequest
{
    public int Id { get; set; }

    public string? Title { get; set; }
}

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
{
    private readonly IGenericRepository<TodoList> _todoListRpository;

    public UpdateTodoListCommandHandler(IGenericRepository<TodoList> todoListRpository)
    {
        _todoListRpository = todoListRpository;
    }


    public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoListRpository.GetById(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        }

        entity.Title = request.Title;

        await _todoListRpository.UpdateAsync(entity, cancellationToken);

        return Unit.Value;
    }
}
