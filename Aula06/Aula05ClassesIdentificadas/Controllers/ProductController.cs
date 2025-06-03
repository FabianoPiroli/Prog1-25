using Microsoft.AspNetCore.Mvc;
using Modelo;
using Repository;

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


            var path = Path.Combine(enviroment.WebRootPath, "TextFiles");

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);


            var filepath = Path.Combine(path, "ProductDelimited.txt");

            //if (!System.IO.File.Exists(filepath))
            //{
                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.WriteLine(fileContent);
                }
            //}

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product c)
        {
            _productRepository.Save(c);
            List<Product> products = _productRepository.RetrieveAll();

            return View("Index", products);
        }
    }
}
