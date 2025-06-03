using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Aula05ClassesIdentificadas.Models;
using Modelo;

namespace Aula05ClassesIdentificadas.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Customer c1 = new Customer();
        c1.Name = "Frodo";
        c1.ObjectCount++;
        Customer.InstanceCount++;

        var c2 = new Customer();
        c2.Name = "Galadriel";
        c2.ObjectCount++;
        Customer.InstanceCount++;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
