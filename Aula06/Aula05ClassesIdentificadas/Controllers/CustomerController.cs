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
            this.enviroment = environment;
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
                fileContent += $"{c.Id}; {c.Name}; {c.HomeAddress?.Id}; {c.HomeAddress?.City}; {c.HomeAddress?.State}; {c.HomeAddress?.Country}; {c.HomeAddress?.StreetLine1}; {c.HomeAddress?.StreetLine2}; {c.HomeAddress?.PostalCode}; {c.HomeAddress?.AddressType}\n";
            }


            var path = Path.Combine(enviroment.WebRootPath, "TextFiles");

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            

            var filepath = Path.Combine(path, "CustomerDelimited.txt");

            //if(!System.IO.File.Exists(filepath))
            //{
                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.WriteLine(fileContent);
                }
            //}

            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer c)
        {
            _customerRepository.Save(c);
            List<Customer> customers = _customerRepository.RetrieveAll();

            return View("Index", customers);
        }


        [HttpPost]
        public IActionResult ImportDelimitatedFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Selecione um arquivo para importar.");
                return View();
            }

            var customers = new List<Customer>();

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                string? line;
                while ((line = stream.ReadLine()) != null)
                {
                    // Esperado: Id; Name; AddressId; City; State; Country; StreetLine1; StreetLine2; PostalCode; AddressType
                    var parts = line.Split(';');
                    if (parts.Length < 10)
                        continue; // Linha inválida

                    var customer = new Customer
                    {
                        Id = int.TryParse(parts[0], out int id) ? id : 0,
                        Name = parts[1].Trim(),
                        HomeAddress = new Address
                        {
                            Id = int.TryParse(parts[2], out int addrId) ? addrId : 0,
                            City = parts[3].Trim(),
                            State = parts[4].Trim(),
                            Country = parts[5].Trim(),
                            StreetLine1 = parts[6].Trim(),
                            StreetLine2 = parts[7].Trim(),
                            PostalCode = parts[8].Trim(),
                            AddressType = parts[9].Trim()
                        }
                    };

                    customers.Add(customer);
                    _customerRepository.Save(customer);
                }
            }

            // Retorna para a lista de clientes após importar
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ImportDelimitatedFile()
        {
            return View();
        }

    }
}
