using Awesome.Entities;
using Awesome.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Awesome.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActionsController : ControllerBase
{
    private readonly AwesomeDbContext _awesomeDbContext;

    public ActionsController(AwesomeDbContext awesomeDbContext)
    {
        _awesomeDbContext = awesomeDbContext;
    }

    [HttpGet]
    public IActionResult Default()
    {
        return Ok();
    }

    [HttpGet("400")]
    public IActionResult Default400()
    {
        return BadRequest();
    }

    [HttpGet("500")]
    public IActionResult Default500()
    {
        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet("time1")]
    public IActionResult Time1()
    {
        Task.Delay(1000).Wait();

        return Ok();
    }

    [HttpGet("time2")]
    public IActionResult Time2()
    {
        Task.Delay(2000).Wait();

        return Ok();
    }

    [HttpGet("time5")]
    public IActionResult Time5()
    {
        Task.Delay(5000).Wait();

        return Ok();
    }

    [HttpGet("cpu1")]
    public IActionResult Cpu1()
    {
        var n = int.MaxValue / 4;

        Task.Delay(1000).Wait();

        Task.WaitAll(
            Task.Run(() =>
            {
                for (var i = 1; i < n; i++) ;
            }));

        Task.Delay(1000).Wait();

        return Ok();
    }

    [HttpGet("cpu2")]
    public IActionResult Cpu2()
    {
        var n = int.MaxValue / 4;

        Task.Delay(1000).Wait();

        Task.WaitAll(
            Task.Run(() =>
            {
                for (var i = 1; i < n; i++) ;
            }),
            Task.Run(() =>
            {
                for (var i = 1; i < n; i++) ;
            }));

        Task.Delay(1000).Wait();

        return Ok();
    }

    [HttpGet("cpu5")]
    public IActionResult Cpu5()
    {
        var n = int.MaxValue / 4;

        Task.Delay(1000).Wait();

        Task.WaitAll(
            Task.Run(() =>
            {
                for (var i = 1; i < n; i++) ;
            }),
            Task.Run(() =>
            {
                for (var i = 1; i < n; i++) ;
            }),
            Task.Run(() =>
            {
                for (var i = 1; i < n; i++) ;
            }),
            Task.Run(() =>
            {
                for (var i = 1; i < n; i++) ;
            }),
            Task.Run(() =>
            {
                for (var i = 1; i < n; i++) ;
            }));

        Task.Delay(1000).Wait();

        return Ok();
    }

    [HttpGet("memory1")]
    public IActionResult Memory1()
    {
        Task.Delay(1000).Wait();

        _ = new int[100_000_000];

        Task.Delay(1000).Wait();

        GC.Collect();

        return Ok();
    }

    [HttpGet("memory2")]
    public IActionResult Memory2()
    {
        Task.Delay(1000).Wait();

        _ = new int[200_000_000];

        Task.Delay(1000).Wait();

        GC.Collect();

        return Ok();
    }

    [HttpGet("memory5")]
    public IActionResult Memory5()
    {
        Task.Delay(1000).Wait();

        _ = new int[500_000_000];

        Task.Delay(1000).Wait();

        GC.Collect();

        return Ok();
    }

    [HttpGet("exception")]
    public IActionResult Exception()
    {
        throw new Exception();
    }

    [HttpGet("google")]
    public IActionResult Google()
    {
        using var httpClient = new HttpClient();

        httpClient.GetAsync("https://google.com").Wait();

        return Ok();
    }

    [HttpGet("database")]
    public IActionResult Database()
    {
        var random = new Random();

        //var todoListId = random.Next(_awesomeDbContext.TodoLists
        //    .AsQueryable()
        //    .Count());

        //_awesomeDbContext.TodoLists
        //    .AsQueryable()
        //    .Where(x => x.Id > todoListId)
        //    .ToList();

        //_awesomeDbContext.TodoLists
        //    .AsQueryable()
        //    .Include(x => x.TodoItems)
        //    .Where(x => x.Id == todoListId)
        //    .ToList();

        //_awesomeDbContext.TodoLists
        //    .AsQueryable()
        //    .Where(x => x.Status == ETodoListStatus.Planned)
        //    .ToList();

        //_awesomeDbContext.TodoLists
        //    .AsQueryable()
        //    .Where(x => x.Status == ETodoListStatus.Started)
        //    .ToList();

        //_awesomeDbContext.TodoLists
        //    .AsQueryable()
        //    .Include(x => x.TodoItems.Where(y => y.Status == ETodoItemStatus.Planned))
        //    .Where(x => x.Status == ETodoListStatus.Planned
        //                && x.TodoItems.Any(y => y.Status == ETodoItemStatus.InProgress))
        //    .ToList();

        //var todoItemId = random.Next(1000000);

        //_awesomeDbContext.TodoItems
        //    .AsQueryable()
        //    .Select(x => new
        //    {
        //        x.Id,
        //        x.Name,
        //        x.TodoListId,
        //        TodoListName = x.TodoList.Name,
        //    })
        //    .Where(x => x.Id > todoItemId)
        //    .OrderBy(x => x.TodoListId)
        //    .Take(random.Next(100))
        //    .ToList();

        //_awesomeDbContext.TodoLists
        //    .AsQueryable()
        //    .Include(x => x.TodoItems.Where(y => y.Status == ETodoItemStatus.Planned))
        //    .Where(x => x.Status == ETodoListStatus.Planned
        //                && x.TodoItems.Any(y => y.Status == ETodoItemStatus.InProgress))
        //    .ToList();

        _awesomeDbContext.TodoLists
            .AsQueryable()
            .Select(x => new
            {
                TodoList = x,
                PlannedItemsCount = x.TodoItems.Count(y => y.Status == ETodoItemStatus.Planned),
            })
            .ToList();

        //var letters = "qwertyuiopasdfghjklzxcvbnm";

        //_awesomeDbContext.TodoLists.Add(new TodoList
        //{
        //    CreatedDate = DateTime.UtcNow,
        //    IsDeleted = random.Next(2) == 1,
        //    Name = new string(Enumerable.Repeat(0, random.Next(10)).Select(x => letters[random.Next(letters.Length)]).ToArray()),
        //    Status = (ETodoListStatus)(random.Next(4) + 1),
        //});

        //_awesomeDbContext.TodoItems.Add(new TodoItem
        //{
        //    CreatedDate = DateTime.UtcNow,
        //    IsDeleted = random.Next(2) == 1,
        //    Name = new string(Enumerable.Repeat(0, random.Next(10)).Select(x => letters[random.Next(letters.Length)]).ToArray()),
        //    Description = new string(Enumerable.Repeat(0, random.Next(20)).Select(x => letters[random.Next(letters.Length)]).ToArray()),
        //    Status = (ETodoItemStatus)(random.Next(4) + 1),
        //    TodoListId = todoListId,
        //});

        //_awesomeDbContext.SaveChanges();

        return Ok();
    }
}
