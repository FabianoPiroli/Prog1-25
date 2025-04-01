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
            Estrutura sint�tica do IF
            if(express�o booleana)
            {
                Senten�a de c�digo a ser executada, caso a condi��o seja verdadeira
            }
            
            Caso o IF tenha apenas uma linha de comando a ser executada na condicional, n�o h� necessidade de utilizar chaves

            if(express�o booleana)
                Apenas um comando
         */

        string retorno = string.Empty;
        //int x = 10;

        if(x < 9)
            retorno =  "X � maior que 9";

        //x = 8;
        if (x > 9)
            retorno = "X � maior que 9";
        else
            retorno = "X � menor que 9";

        //x = 11;
        if (x == 10) 
            {
                retorno = " Ora ora";
                retorno += " X � igual a 10";
            }
        else if(x == 9)
        {
            retorno = "Hmmm";
            retorno += " X � igual a 9";
        }
        else if(x == 8)
        {
            retorno = "bahhh";
            retorno += " X � igual a 8 tch�.";
        }
        else
        {
            retorno = "Sei l� que n�mero � x";
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
                retorno = "X � zero";
                break;
            case 1:
                retorno = "X � um";
                break;
            case 2:
                retorno = "X � dois";
                break;
            case 3:
                retorno = "X � tr�s";
                break;
            default:
                retorno = "X � algum n�mero n�o previsto";
                break;
        }

        return retorno;
    }

    [HttpGet]
    public string GetFor(int x)
    {
        /*
            O comando de repeti��o FOR possui a seguinte sitaxe:
            for(<inicializador>; <express�o condicional>; <express�o de repeti��o>)
            {
                Comandos a serem executada
            }
            Inicializador: elemento contador. Tradicionalmente utilizado i = �ndice
            Express�o condicional: Especifica o teste a ser verificado quando o loop estiver executando o n�mero definido de itera��es;
            Express�o de repeti��o: Especifica a a��o a ser executada com a vari�vel contadora.
            Geralmente um ac�mulo ou decr�scimo (acumulador);
         */

        string retorno = string.Empty;
        for (int i = 1; i < x; i++)
        {
            // E se eu quisesse interromper o la�o caso ele fosse maior que 5?
            if(i > 50)
                break; // O comando break interrompe a execu��o do la�o

            // Caso eu deseje que o la�o siga em frente, for�ando a continuar a execu��o
            if ((i % 2) != 0)
                continue;
            retorno += $"{i};";
        }
        return retorno;
    }

    [HttpGet]
    public string GetForeach(string color)
    {
        /*
            I comando foreach (para cada) � utilizado para iterar por uma sequ�ncia de itens de uma cole��o e servir
            como uma op��o simples de repeti��o.
         */

        string[] colors = { "Vermelho", "Preto", "Azul", "Amarelo", "Verde", "Branco", "Azul-Marinho", "Rosa", "Roxo", "Cinza" };

        string retorno = string.Empty;
        if (colors.Contains(char.ToUpper(color[0]) + color.Substring(1)))
            retorno = $"A cor escolhida � v�lida!";
        else
            retorno = $"A cor escolhida n�o � v�lida!";

        foreach (string s in colors)
        {
            retorno += $" [{s}] ";
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
