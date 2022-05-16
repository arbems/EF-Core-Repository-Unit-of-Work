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
    private readonly IRepositoryBase<TodoList> _todoListRepository;
    private readonly IReadRepositoryBase<TodoList> _todoListReadRepository;

    public UpdateTodoListCommandHandler(IRepositoryBase<TodoList> todoListRepository, IReadRepositoryBase<TodoList> todoListReadRepository)
    {
        _todoListRepository = todoListRepository;
        _todoListReadRepository = todoListReadRepository;
    }


    public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoListReadRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        }

        entity.Title = request.Title;

        await _todoListRepository.UpdateAsync(entity, cancellationToken);

        return Unit.Value;
    }
}
