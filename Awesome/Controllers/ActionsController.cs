﻿using Microsoft.AspNetCore.Mvc;

namespace Awesome.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActionsController : ControllerBase
{
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
        Task.WaitAll(
            Task.Run(() =>
            {
                for (var i = 1; i < int.MaxValue; i++) ;
            }));

        return Ok();
    }

    [HttpGet("cpu2")]
    public IActionResult Cpu2()
    {
        Task.WaitAll(
            Task.Run(() =>
            {
                for (var i = 1; i < int.MaxValue; i++) ;
            }),
            Task.Run(() =>
            {
                for (var i = 1; i < int.MaxValue; i++) ;
            }));

        return Ok();
    }

    [HttpGet("cpu5")]
    public IActionResult Cpu5()
    {
        Task.WaitAll(
            Task.Run(() =>
            {
                for (var i = 1; i < int.MaxValue; i++) ;
            }),
            Task.Run(() =>
            {
                for (var i = 1; i < int.MaxValue; i++) ;
            }),
            Task.Run(() =>
            {
                for (var i = 1; i < int.MaxValue; i++) ;
            }),
            Task.Run(() =>
            {
                for (var i = 1; i < int.MaxValue; i++) ;
            }),
            Task.Run(() =>
            {
                for (var i = 1; i < int.MaxValue; i++) ;
            }));

        return Ok();
    }

    [HttpGet("memory1")]
    public IActionResult Memory1()
    {
        _ = new int[100_000_000];

        GC.Collect();

        return Ok();
    }

    [HttpGet("memory2")]
    public IActionResult Memory2()
    {
        _ = new int[200_000_000];

        GC.Collect();

        return Ok();
    }

    [HttpGet("memory5")]
    public IActionResult Memory5()
    {
        _ = new int[500_000_000];

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
}