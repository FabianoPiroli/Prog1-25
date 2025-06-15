using Microsoft.AspNetCore.Mvc;
using Modelo;
using Repository;
using System.Xml.Linq;

namespace Aula05ClassesIdentificadas.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment enviroment;

        private ProductRepository _productRepository;

        public ProductController(
            IWebHostEnvironment environment
        )
        {
            _productRepository = new ProductRepository();
            this.enviroment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = _productRepository.RetrieveAll();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ExportDelimitatedFile()
        {
            string fileContent = string.Empty;
            foreach (Product p in ProductData.Products)
            {
                fileContent += $"{p.Id}; {p.ProductName}; {p.Description}; {p.CurrentPrice};\n";
            }


            SaveFile(fileContent, "ProductsDelimitated.txt");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {
            _productRepository.Save(p);
            List<Product> products = _productRepository.RetrieveAll();

            return View("Index", products);
        }
        [HttpPost]
        public IActionResult ImportDelimitatedFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Selecione um arquivo para importar.");
                return View();
            }

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                string? line;
                while ((line = stream.ReadLine()) != null)
                {
                    // Esperado: Id; ProductName; Description; CurrentPrice;
                    var parts = line.Split(';');
                    if (parts.Length < 4)
                        continue; // Linha inválida

                    var product = new Product
                    {
                        Id = int.TryParse(parts[0], out int id) ? id : 0,
                        ProductName = parts[1].Trim(),
                        Description = parts[2].Trim(),
                        CurrentPrice = decimal.TryParse(parts[3], out decimal price) ? price : 0
                    };

                    _productRepository.Save(product);
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ImportDelimitatedFile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ExportFixedFile()
        {
            string fileContent = string.Empty;
            foreach (Product p in ProductData.Products)
            {
                fileContent += 
                    String.Format("{0:5}", p.Id) +
                    String.Format("{0:30}", p.ProductName) +
                    String.Format("{0:50}", p.Description) +
                    String.Format("{0:10}", p.CurrentPrice) + "\n";

            }
            SaveFile(fileContent, "ProductsFixed.txt");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            Product product = _productRepository.Retrieve(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            if (!_productRepository.DeleteById(id.Value))

                return NotFound();
            
            return RedirectToAction("Index");
        }

        private bool SaveFile(string content, string fileName)
        {
            bool ret = false;

            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(fileName))
                ret = false;

            var path = Path.Combine(enviroment.WebRootPath, "TextFiles");

            try
            {
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);


                var filepath = Path.Combine(path, fileName);

                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.WriteLine(content);
                }
            }
            catch (IOException ioEx)
            {
                string msg = ioEx.Message;
                ret = false;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                ret = false;
            }
            return ret;
        }

    }
}
