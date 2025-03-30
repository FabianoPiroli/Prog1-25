using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Aula03.Models;

namespace Aula03.Controllers;

public class HomeController : Controller
{
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
    [HttpGet]
    public string getIf(int x)
    {
        /*
            Estrutura sintática do IF
            if(expressão booleana)
            {
                Sentença de código a ser executada, caso a condição seja verdadeira
            }
            
            Caso o IF tenha apenas uma linha de comando a ser executada na condicional, não há necessidade de utilizar chaves

            if(expressão booleana)
                Apenas um comando
         */

        string retorno = string.Empty;
        //int x = 10;

        if(x < 9)
            retorno =  "X é maior que 9";

        //x = 8;
        if (x > 9)
            retorno = "X é maior que 9";
        else
            retorno = "X é menor que 9";

        //x = 11;
        if (x == 10) 
            {
                retorno = " Ora ora";
                retorno += " X é igual a 10";
            }
        else if(x == 9)
        {
            retorno = "Hmmm";
            retorno += " X é igual a 9";
        }
        else if(x == 8)
        {
            retorno = "bahhh";
            retorno += " X é igual a 8 tchê.";
        }
        else
        {
            retorno = "Sei lá que número é x";
        }


        return retorno;
    }

    [HttpGet]
    public string GetSwitch(int x)
    {
        string retorno = string.Empty;
        switch (x)
        {
            case 0:
                retorno = "X é zero";
                break;
            case 1:
                retorno = "X é um";
                break;
            case 2:
                retorno = "X é dois";
                break;
            case 3:
                retorno = "X é três";
                break;
            default:
                retorno = "X é algum número não previsto";
                break;
        }

        return retorno;
    }

    [HttpGet]
    public string GetFor(int x)
    {
        /*
            O comando de repetição FOR possui a seguinte sitaxe:
            for(<inicializador>; <expressão condicional>; <expressão de repetição>)
            {
                Comandos a serem executada
            }
            Inicializador: elemento contador. Tradicionalmente utilizado i = índice
            Expressão condicional: Especifica o teste a ser verificado quando o loop estiver executando o número definido de iterações;
            Expressão de repetição: Especifica a ação a ser executada com a variável contadora.
            Geralmente um acúmulo ou decréscimo (acumulador);
         */

        string retorno = string.Empty;
        for (int i = 0; i < x; i++)
        {
            retorno += $"{i};";
        }
        return retorno;
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
