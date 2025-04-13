using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Aula04Recursividade.Models;

namespace Aula04Recursividade.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
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

    [HttpGet]
    public string PrintNaturalFor(int n = 10)
    {
        string retorno = string.Empty;

        int i = 1;
        while (i <= n)
        {
            retorno += $"{i} ";
            i++;
        }

        return retorno;
    }

    [HttpGet]
    public string PrintNaturalRecursion(int count = 10)
    {
        string retorno = string.Empty;

        retorno = NaturalNumberRecursion(1, count);

        return retorno;
    }

    public string NaturalNumberRecursion(int n, int count)
    {
        string ret = string.Empty;

        // Caso Base: se o contador for menor que 1
        if (count < 1)
            return $" {n} ";

        ret += $" {n} ";
        count--; // Decrementa count

        // Chamada recursiva: incrementa n e decrementa count para imprimir o número
        ret += NaturalNumberRecursion(n + 1, count);

        return ret;
    }

    // 1 - Escreva um programa recursivo em C# para imprimir os número de n até 1 (decrescente).
    [HttpGet]
    public string PrintDecrease(int n = 1)
    {
        string retorno = string.Empty;

        int i = 20;
        while (i >= n)
        {
            retorno += $"{i} ";
            i--;
        }

        return retorno;
    }

    // 2 - Escreva um programa em C# capaz de sumarizar os números de 1 a n, por exemplo : n = 10 [1+2+3+4+5+6+7+8+9+10]
    [HttpGet]
    public int PrintSumarize(int n = 1)
    {
        return RecursiveSum(n);
    }
    public int RecursiveSum(int n)
    {
        if (n >= 10)
            return n;
        return n + RecursiveSum(n + 1);
    }

    // 3 - Escreva um programa em C# capaz de contar quantos caracteres tem uma string;
    [HttpGet]
    public string CharacterCount()
    {
        string str = "Aula04Recursividade";
        int count = CountCharactersRecursive(str);

        return $"A string {str} tem {count} caracteres.";
    }

    public int CountCharactersRecursive(string str)
    {
        if (string.IsNullOrEmpty(str))
            return 0;

        return 1 + CountCharactersRecursive(str.Substring(1));
    }

    // 4 - Escreva um programa recursivo que seja capaz de identificar se uma palavra é ou não um palíndromo. Ex: ovo, radar, arara etc.
    [HttpGet]
    public string Palindrome(string str = "arara")
    {
        bool isPalindrome = IsPalindrome(str);
        return isPalindrome ? $"{str} é um palíndromo." : $"{str} não é um palíndromo.";
    }
    public bool IsPalindrome(string str)
    {
        if (str.Length <= 1)
            return true;
        if (str[0] != str[str.Length - 1])
            return false;
        return IsPalindrome(str.Substring(1, str.Length - 2));
    }
}
