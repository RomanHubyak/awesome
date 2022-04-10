using Awesome.Entities;
using Awesome.Enums;
using Microsoft.EntityFrameworkCore;

namespace Awesome;

public class AwesomeDbContext : DbContext
{
    public DbSet<TodoList> TodoLists { get; set; }

    public DbSet<TodoItem> TodoItems { get; set; }

    public AwesomeDbContext(DbContextOptions<AwesomeDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var n = 10;
        var m = 100;

        var letters = "qwertyuiopasdfghjklzxcvbnm";

        var random = new Random();

        for (var i = 1; i <= n; i++)
        {
            modelBuilder.Entity<TodoList>().HasData(new TodoList
            {
                Id = i,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = random.Next(2) == 1,
                Name = new string(Enumerable.Repeat(0, random.Next(10)).Select(x => letters[random.Next(letters.Length)]).ToArray()),
                Status = (ETodoListStatus)(random.Next(4) + 1),
            });

            for (var j = 1; j <= m; j++)
            {
                modelBuilder.Entity<TodoItem>().HasData(new TodoItem
                {
                    Id = (i - 1) * m + j,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = random.Next(2) == 1,
                    Name = new string(Enumerable.Repeat(0, random.Next(10)).Select(x => letters[random.Next(letters.Length)]).ToArray()),
                    Description = new string(Enumerable.Repeat(0, random.Next(20)).Select(x => letters[random.Next(letters.Length)]).ToArray()),
                    Status = (ETodoItemStatus)(random.Next(4) + 1),
                    TodoListId = i,
                });
            }
        }
    }
}
