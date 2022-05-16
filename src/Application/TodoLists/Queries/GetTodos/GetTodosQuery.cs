using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TodoLists.Queries.GetTodos;

public class GetTodosQuery : IRequest<TodosVm>
{
}

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
{
    private readonly IReadRepositoryBase<TodoList> _todoListReadRepository;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(IReadRepositoryBase<TodoList> todoListReadRepository, IMapper mapper)
    {
        _todoListReadRepository = todoListReadRepository;
        _mapper = mapper;
    }

    public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        return new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                .ToList(),

            Lists = await _todoListReadRepository.GetAll()
                .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken)
        };
    }
}
