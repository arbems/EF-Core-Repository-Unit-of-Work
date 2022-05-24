using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class TodoItem : Entity
{
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

    public int ListId { get; set; }
    public TodoList List { get; set; } = null!;

    public TodoItem() { }

    public TodoItem(string? title, bool note, PriorityLevel priority, DateTime? reminder)
    {
        Title = title;
        Done = note;
        Priority = priority;
        Reminder = reminder;
    }
}
