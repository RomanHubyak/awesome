using Awesome.Entities;
using Awesome.Enums;
using Microsoft.EntityFrameworkCore;

namespace Awesome;

public class SqlHostedServer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public SqlHostedServer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Run();
        Run2();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async void Run()
    {
        while (true)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var awesomeDbContext = scope.ServiceProvider.GetService<AwesomeDbContext>();

                var random = new Random();

                var todoListId = random.Next(await awesomeDbContext.TodoLists
                    .AsQueryable()
                    .CountAsync());

                await awesomeDbContext.TodoLists
                    .AsQueryable()
                    .Where(x => x.Id > todoListId)
                    .ToListAsync();

                await awesomeDbContext.TodoLists
                    .AsQueryable()
                    .Include(x => x.TodoItems)
                    .Where(x => x.Id == todoListId)
                    .ToListAsync();

                await awesomeDbContext.TodoLists
                    .AsQueryable()
                    .Where(x => x.Status == ETodoListStatus.Completed)
                    .ToListAsync();

                await awesomeDbContext.TodoLists
                    .AsQueryable()
                    .Where(x => x.Status == ETodoListStatus.Canceled)
                    .ToListAsync();

                await awesomeDbContext.TodoLists
                    .AsQueryable()
                    .Include(x => x.TodoItems.Where(y => y.Status == ETodoItemStatus.Planned))
                    .Where(x => x.Status == ETodoListStatus.Planned
                                && x.TodoItems.Any(y => y.Status == ETodoItemStatus.InProgress))
                    .ToListAsync();

                var todoItemId = random.Next(1000000);

                await awesomeDbContext.TodoItems
                    .AsQueryable()
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.TodoListId,
                        TodoListName = x.TodoList.Name,
                    })
                    .Where(x => x.Id > todoItemId)
                    .OrderBy(x => x.TodoListId)
                    .Take(random.Next(100))
                    .ToListAsync();

                var letters = "qwertyuiopasdfghjklzxcvbnm";

                awesomeDbContext.TodoLists.Add(new TodoList
                {
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = random.Next(2) == 1,
                    Name = new string(Enumerable.Repeat(0, random.Next(10)).Select(x => letters[random.Next(letters.Length)]).ToArray()),
                    Status = (ETodoListStatus)(random.Next(4) + 1),
                });

                awesomeDbContext.TodoItems.Add(new TodoItem
                {
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = random.Next(2) == 1,
                    Name = new string(Enumerable.Repeat(0, random.Next(10)).Select(x => letters[random.Next(letters.Length)]).ToArray()),
                    Description = new string(Enumerable.Repeat(0, random.Next(20)).Select(x => letters[random.Next(letters.Length)]).ToArray()),
                    Status = (ETodoItemStatus)(random.Next(4) + 1),
                    TodoListId = todoListId,
                });

                await awesomeDbContext.SaveChangesAsync();
            }
            catch
            {
            }
        }
    }

    private async void Run2()
    {
        while (true)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var awesomeDbContext = scope.ServiceProvider.GetService<AwesomeDbContext>();

                await awesomeDbContext.TodoLists
                    .AsQueryable()
                    .Include(x => x.TodoItems.Where(y => y.Status == ETodoItemStatus.Planned))
                    .Where(x => x.Status == ETodoListStatus.Planned
                                && x.TodoItems.Any(y => y.Status == ETodoItemStatus.InProgress))
                    .ToListAsync();
            }
            catch
            {
            }
        }
    }
}
