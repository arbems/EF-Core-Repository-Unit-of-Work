using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Persistence;

public static class ApplicationContextSeed
{
    public static async Task SeedSampleDataAsync(ApplicationContext context)
    {
        // Seed, if necessary
        if (!context.TodoLists.Any())
        {
            var todoList = new TodoList
            {
                Id = 1,
                Title = "Shopping",
                Colour = Colour.Blue
            };

            context.TodoLists.Add(todoList);

            todoList.AddItem("Apples", true, Domain.Enums.PriorityLevel.Medium, DateTime.UtcNow);
            todoList.AddItem("Milk", true, Domain.Enums.PriorityLevel.Medium, DateTime.UtcNow);
            todoList.AddItem("Bread", true, Domain.Enums.PriorityLevel.High, DateTime.UtcNow);
            todoList.AddItem("Pasta", false, Domain.Enums.PriorityLevel.None, DateTime.UtcNow);
            todoList.AddItem("Tissues", false, Domain.Enums.PriorityLevel.Medium, DateTime.UtcNow);
            todoList.AddItem("Tuna", false, Domain.Enums.PriorityLevel.None, DateTime.UtcNow);
            todoList.AddItem("Water", false, Domain.Enums.PriorityLevel.Medium, DateTime.UtcNow);

            await context.SaveChangesAsync();
        }
    }
}
