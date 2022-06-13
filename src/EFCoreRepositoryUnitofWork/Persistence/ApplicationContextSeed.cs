using EFCoreRepositoryUnitofWork.Entities;

namespace EFCoreRepositoryUnitofWork.Persistence;

public static class ApplicationContextSeed
{
    public static async Task SeedSampleDataAsync(ApplicationContext context)
    {
        // Seed, if necessary
        if (!context.Users.Any())
        {
            var user = new User
            {
                Id = 1,
                Name = "Alberto",
                Email = "contacto@arbems.com",
                Username = "arbems",
                Address = new Address("Calle sierpes", "Sevilla", "Sevilla", "España", "41005"),
                Posts =
                {
                    new Post {  Id = 1,
                        Title = "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
                        Body = "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto",
                        UserId = 1,
                        Comments = {
                            new Comment { Id = 1, Name = "Oscar", Body = "id labore ex et quam laborum", Email = "Lew@alysha.tv", PostId = 1},
                            new Comment { Id = 2, Name = "Jesus", Body = "odio adipisci rerum aut animi", Email = "Hayden@althea.biz", PostId = 1},
                            new Comment { Id = 3, Name = "Marta", Body = "vero eaque aliquid doloribus et culpa", Email = "Presley.Mueller@myrl.com", PostId = 1},
                        }
                    },
                    new Post {  Id = 2,
                        Title = "qui est esse",
                        Body = "st rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus ",
                        UserId = 1,
                        Comments = {
                            new Comment { Id = 4, Name = "Ana", Body = "et fugit eligendi deleniti quidem qui sint nihil autem", Email = "Dallas@ole.me", PostId = 2},
                        }
                    },
                    new Post {  Id = 3,
                        Title = "ea molestias quasi exercitationem repellat qui ipsa sit aut",
                        Body = "et iusto sed quo iure\nvoluptatem occaecati omnis eligendi aut ad\nvoluptatem doloribus vel accusantium quis pariatur\nmolestiae porro eius odio et labore et velit aut",
                        UserId = 1,
                        Comments = {
                            new Comment { Id = 5, Name = "Elena", Body = "et omnis dolorem", Email = "Mallory_Kunze@marie.org", PostId = 3},
                            new Comment { Id = 6, Name = "Carlos", Body = "provident id voluptas", Email = "Meghan_Littel@rene.us", PostId = 3},
                        }
                    }
                }
            };

            context.Users.Add(user);

            await context.SaveChangesAsync();
        }
    }
}
