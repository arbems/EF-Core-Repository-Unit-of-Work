using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;

namespace Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemBriefDto>>
{
    public int ListId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemBriefDto>>
{
    private readonly IReadRepositoryBase<TodoItem> _todoItemReadRepository;
    private readonly IMapper _mapper;

    public GetTodoItemsWithPaginationQueryHandler(IReadRepositoryBase<TodoItem> todoItemReadRepository, IMapper mapper)
    {
        _todoItemReadRepository = todoItemReadRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TodoItemBriefDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _todoItemReadRepository
            .GetAllBySpec(x => x.ListId == request.ListId)
            .OrderBy(x => x.Title)
            .ProjectTo<TodoItemBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
