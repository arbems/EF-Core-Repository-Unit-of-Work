using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class TodoItem : AuditableEntity
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }

    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            _done = value;
        }
    }

    public TodoList List { get; set; } = null!;
}
