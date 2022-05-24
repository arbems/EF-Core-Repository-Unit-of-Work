using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .OwnsOne(b => b.Colour);

        // la llamada a SetPropertyAccessMode indica a EF Core 
        // que debe acceder a la propiedad Items a través de su campo.
        var navigation = builder.Metadata.FindNavigation(nameof(TodoList.Items));
        //EF accede a la propiedad de colección de elementos a través de su campo de respaldo
        navigation!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
