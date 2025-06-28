using Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Imobiliaria.Controllers
{
    public class ImovelController : Controller
    {
        public static List<Categoria> categorias = new List<Categoria>
    {
        new Categoria { Id = 1, Nome = "Apartamento" },
        new Categoria { Id = 2, Nome = "Casa" },
        new Categoria { Id = 3, Nome = "Sítio" },
        new Categoria { Id = 4, Nome = "Sala Comercial" }
    };

        public static List<Imovel> imoveis = new List<Imovel>();

        [HttpGet]
        public IActionResult Index()
        {
            var lista = imoveis.Select(i =>
            {
                i.Categoria = categorias.FirstOrDefault(c => c.Id == i.CategoriaId);
                return i;
            }).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Imovel imovel, IFormFile Imagem)
        {
            if (Imagem != null && Imagem.Length > 0)
            {
                var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(Imagem.FileName);
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens");
                Directory.CreateDirectory(caminhoPasta);
                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    Imagem.CopyTo(stream);
                }

                imovel.CaminhoImagem = "/imagens/" + nomeArquivo;
            }

            imovel.Categoria = categorias.FirstOrDefault(c => c.Id == imovel.CategoriaId);
            imoveis.Add(imovel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExportarTxt()
        {
            var linhas = imoveis.Select(i =>
                $"\"{i.Nome}\";\"{i.Endereco}\";\"{i.Quartos}\";\"{i.VagasGaragem}\";\"{i.Banheiros}\";\"{i.Descricao}\";\"{categorias.First(c => c.Id == i.CategoriaId).Nome}\""
            ).ToList();

            string caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imoveis.txt");
            System.IO.File.WriteAllLines(caminho, linhas);

            return File(System.IO.File.ReadAllBytes(caminho), "text/plain", "imoveis.txt");
        }

        [HttpGet]
        public IActionResult Update(Imovel i)
        {

            return View();
        }


    }
}
