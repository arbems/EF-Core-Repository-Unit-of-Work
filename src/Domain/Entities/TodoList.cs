using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class TodoList : Entity
{
    public string? Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;

    private readonly List<TodoItem> _items = new();
    public IReadOnlyCollection<TodoItem> Items => _items;

    public TodoList() { }

    public TodoList(string title, Colour colour)
    {
        Title = title;
        Colour = colour;
    }

    public void AddItem(string? title, bool note, PriorityLevel priority, DateTime? reminder)
    {
        // Validation logic...

        var item = new TodoItem(title, note, priority, reminder);
        _items.Add(item);
    }
}
