using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeekDays.Models;

namespace WeekDays.Controllers;

public class Result
{
    public string Dia = string.Empty;
}
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public string WeekDays(int x)
    {
        string[] weekDays = { "Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado" };
        if (x < 1 || x > 7)
        {
            return ("Dia da semana inválido");
        }

        
        return weekDays[x - 1];
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View("Index", new Result());
    }

    [HttpPost]
    public IActionResult Privacy(int dia)
    {
        Result resultado = new()
        {
            Dia = WeekDays(dia)
        };
        return View("Index", resultado);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
