using Microsoft.AspNetCore.Mvc;

namespace Aula01.Controllers
{

    public class Result
    {
        public string Texto = string.Empty;
    }
    public class TesteController : Controller
    {
        private readonly ILogger<TesteController> _logger;

        public TesteController(
            ILogger<TesteController> logger

        )
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index", new Result());
        }

        [HttpPost]
        public IActionResult Index(string texto)
        {
            Result resultado = new();
            resultado.Texto = AplicarCifraDeCesar(texto, 3);

            return View("Index", resultado);
        }

        private string AplicarCifraDeCesar(string texto, int incremento)
        {
            char[] textoCifrado = texto.ToCharArray();
            for (int i = 0; i < textoCifrado.Length; i++)
            {
                char c = textoCifrado[i];
                if (char.IsLetter(c))
                {
                    char baseChar = char.IsUpper(c) ? 'A' : 'a';
                    textoCifrado[i] = (char)((((c - baseChar) + incremento) % 26) + baseChar);
                }
            }
            return new string(textoCifrado);
        }
    }
}
