using Modelo;
using Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Imobiliaria.Controllers
{
    public class ImovelController : Controller
    {
        private readonly IWebHostEnvironment enviroment;
        private ImovelRepository _imovelRepository;

        public ImovelController(IWebHostEnvironment enviroment)
        {
            _imovelRepository = new ImovelRepository();
            this.enviroment = enviroment;
        }

        public static List<Categoria> categorias = new List<Categoria>
            {
                new Categoria { Id = 1, Nome = "Apartamento" },
                new Categoria { Id = 2, Nome = "Casa" },
                new Categoria { Id = 3, Nome = "Sítio" },
                new Categoria { Id = 4, Nome = "Sala Comercial" }
            };

        [HttpGet]
        public IActionResult Index()
        {
            var lista = _imovelRepository.RetrieveAll();
            foreach (var i in lista)
            {
                i.Categoria = categorias.FirstOrDefault(c => c.Id == i.CategoriaId);
            }
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
            _imovelRepository.Save(imovel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExportarTxt()
        {
            var lista = _imovelRepository.RetrieveAll();
            var linhas = lista.Select(i =>
                $"{i.Nome}; {i.Endereco.Logradouro}; {i.Endereco.Referencia}; {i.Endereco.Cidade}; {i.Endereco.Estado}; {i.Endereco.CEP}; {i.Endereco.Pais}; Quartos: {i.Quartos}; Vagas: {i.VagasGaragem}; Banheiros: {i.Banheiros}; {i.Descricao}; {categorias.First(c => c.Id == i.CategoriaId).Nome}\n"
            ).ToList();

            string caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imoveis.txt");
            System.IO.File.WriteAllLines(caminho, linhas);

            return File(System.IO.File.ReadAllBytes(caminho), "text/plain", "imoveis.txt");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id <= 0)
                return NotFound();

            var imovel = _imovelRepository.Retrieve(id.Value);
            if (imovel == null)
                return NotFound();

            return View(imovel);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id is null || id <= 0)
                return NotFound();

            var imovel = _imovelRepository.Retrieve(id.Value);
            if (imovel == null)
                return NotFound();

            if (!string.IsNullOrEmpty(imovel.CaminhoImagem))
            {
                var caminhoFisico = Path.Combine(enviroment.WebRootPath, imovel.CaminhoImagem.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(caminhoFisico))
                    System.IO.File.Delete(caminhoFisico);
            }

            if (_imovelRepository.DeleteById(id.Value))
            {
                TempData["MensagemSucesso"] = "Imóvel excluído com sucesso!";
                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Erro ao excluir o imóvel.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null || id <= 0)
                return NotFound();

            Imovel imovel = _imovelRepository.Retrieve(id.Value);

            if (imovel == null)
                return NotFound();

            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome", imovel.CategoriaId);
            return View(imovel);
        }

        [HttpPost]
        public IActionResult ConfirmUpdate(Imovel imovel, IFormFile Imagem, string CaminhoImagem)
        {
            if (Imagem != null && Imagem.Length > 0)
            {

                if (!string.IsNullOrEmpty(CaminhoImagem))
                {
                    var caminhoFisicoAntigo = Path.Combine(enviroment.WebRootPath, CaminhoImagem.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(caminhoFisicoAntigo))
                        System.IO.File.Delete(caminhoFisicoAntigo);
                }

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
            else
            {
                imovel.CaminhoImagem = CaminhoImagem;
            }

            imovel.Categoria = categorias.FirstOrDefault(c => c.Id == imovel.CategoriaId);
            _imovelRepository.Update(imovel);
            TempData["MensagemSucesso"] = "Imóvel atualizado com sucesso!";

            return RedirectToAction("Index");
        }
    }
}
