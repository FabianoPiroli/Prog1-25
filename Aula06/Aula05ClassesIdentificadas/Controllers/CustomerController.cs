using Microsoft.AspNetCore.Mvc;
using Modelo;
using Repository;

namespace Aula05ClassesIdentificadas.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IWebHostEnvironment enviroment;

        private CustomerRepository _customerRepository;

        public CustomerController(
            IWebHostEnvironment environment
        )
        {
            _customerRepository = new CustomerRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Customer> customers = _customerRepository.RetrieveAll();

            return View(customers);
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
            foreach (Customer c in CustomerData.Customers)
            {
                fileContent +=
                                @$"{c.Id};
                                  {c.Name};
                                  {c.HomeAddress?.Id};
                                  {c.HomeAddress?.City};
                                  {c.HomeAddress?.State};
                                  {c.HomeAddress?.Country};
                                  {c.HomeAddress?.StreetLine1};
                                  {c.HomeAddress?.StreetLine2};
                                  {c.HomeAddress?.PostalCode};
                                  {c.HomeAddress?.AddressType}
                                   \n
                               ";
            }

            var filepath = Path.Combine(enviroment.WebRootPath, "TextFiles", "Delimitado.txt");

            if(!System.IO.File.Exists(filepath))
            {
                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.WriteLine(fileContent);
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer c)
        {
            _customerRepository.Save(c);
            List<Customer> customers = _customerRepository.RetrieveAll();

            return View("Index", customers);
        }

        
    }
}
