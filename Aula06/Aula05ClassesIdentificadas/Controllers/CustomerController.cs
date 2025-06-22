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
        public IActionResult ExportDelimitatedFile()
        {
            string fileContent = string.Empty;
            foreach (Customer c in CustomerData.Customers)
            {
                fileContent += $"{c.Id}; {c.Name}; {c.HomeAddress?.Id}; {c.HomeAddress?.City}; {c.HomeAddress?.State}; {c.HomeAddress?.Country}; {c.HomeAddress?.StreetLine1}; {c.HomeAddress?.StreetLine2}; {c.HomeAddress?.PostalCode}; {c.HomeAddress?.AddressType}\n";
            }

            SaveFile(fileContent, "CustomersDelimitated.txt");

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
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

        [HttpGet]
        public IActionResult ExportFixedFile()
        {
            string fileContent = string.Empty;
            foreach (Customer c in CustomerData.Customers)
            {
                fileContent +=
                    String.Format("{0:5}", c.Id) +
                    String.Format("{0:64}", c.Name) +
                    String.Format("{0:5}", c.HomeAddress!.Id) +
                    String.Format("{0:32}", c.HomeAddress!.City) +
                    String.Format("{0:2}", c.HomeAddress!.State) +
                    String.Format("{0:3}", c.HomeAddress!.Country) +
                    String.Format("{0:64}", c.HomeAddress!.StreetLine1) +
                    String.Format("{0:64}", c.HomeAddress!.StreetLine2) +
                    String.Format("{0:9}", c.HomeAddress!.PostalCode) +
                    String.Format("{0:16}", c.HomeAddress!.AddressType) +
                    "\n";
            }

            SaveFile(fileContent, "CustomersFixed.txt");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            Customer customer = _customerRepository.Retrieve(id.Value);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id is null || id.Value <=0)
                return NotFound();

            if(!_customerRepository.DeletById(id.Value))
                return NotFound();

            return RedirectToAction("Index");
        }

        private bool SaveFile(string content, string fileName)
        {
            bool ret = true;

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

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is null || id.Value <= 0)
                return NotFound();

            Customer customer = _customerRepository.Retrieve(id.Value);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult ConfirmUpdate(int? id)
        {
            return View();
        }
    }
}
