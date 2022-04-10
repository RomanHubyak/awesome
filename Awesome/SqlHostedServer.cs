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
            using var scope = _serviceProvider.CreateScope();

            var awesomeDbContext = scope.ServiceProvider.GetService<AwesomeDbContext>();

            var random = new Random();

            var todoListId = random.Next(await awesomeDbContext.TodoLists
                .AsQueryable()
                .CountAsync());

            await awesomeDbContext.TodoLists
                .AsQueryable()
                .ToListAsync();

            await awesomeDbContext.TodoLists
                .AsQueryable()
                .Include(x => x.TodoItems)
                .Where(x => x.Id == todoListId)
                .ToListAsync();

            await awesomeDbContext.TodoLists
                .AsQueryable()
                .Where(x => x.Status == ETodoListStatus.Planned)
                .ToListAsync();

            await awesomeDbContext.TodoLists
                .AsQueryable()
                .Where(x => x.Status == ETodoListStatus.Started)
                .ToListAsync();

            await awesomeDbContext.TodoLists
                .AsQueryable()
                .Where(x => x.Status == ETodoListStatus.Completed)
                .ToListAsync();

            await awesomeDbContext.TodoLists
                .AsQueryable()
                .Where(x => x.Status == ETodoListStatus.Canceled)
                .ToListAsync();

            await awesomeDbContext.TodoItems
                .AsQueryable()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.TodoListId,
                    TodoListName = x.TodoList.Name,
                })
                .Where(x => x.Id > random.Next(1000000))
                .OrderBy(x => x.TodoListId)
                .Take(random.Next(100000))
                .ToListAsync();
        }
    }
}
