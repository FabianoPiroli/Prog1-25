using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AtividadeRequisitoVeterinaria.Models;

namespace AtividadeRequisitoVeterinaria.Controllers;

public class HomeController : Controller
{
    public class Clinica
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
    }
    public class Animal
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Raca { get; set; }
        public string Cor { get; set; }
        public string Sexo { get; set; }
        public string Peso { get; set; }
        public string Responsavel { get; set; }
    }
    public class Veterinario
    {
        public string Nome { get; set; }
        public string Crmv { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
    }

    public class Atendimento
    {
        public string NomeAnimal { get; set; }
        public string NomeVeterinario { get; set; }
        public DateTime DataConsulta { get; set; }
        public string Motivo { get; set; }
        public string Procedimentos { get; set; }
    }

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
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
