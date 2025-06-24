using Aula05ClassesIdentificadas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using Repository;

namespace Aula05ClassesIdentificadas.Controllers
{
    public class OrderController : Controller
    {
        private readonly IWebHostEnvironment enviroment;
        private readonly OrderRepository _orderRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly ProductRepository _productRepository;
        public OrderController(
            IWebHostEnvironment enviroment
        )
        {
            this.enviroment = enviroment;
            _orderRepository = new OrderRepository();
            _customerRepository = new CustomerRepository();
            _productRepository = new ProductRepository();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_orderRepository.RetrieveAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            OrderViewModel viewModel = new();
            viewModel.Customers = _customerRepository.RetrieveAll();

            var products = _productRepository.RetrieveAll();
            List<SelectedItem> items = [];
            foreach (var product in products) 
            { 
                items.Add(new SelectedItem() 
                { OrderItem = new() 
                { Product = product } });
            }
            viewModel.SelectedItems = items;

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(OrderViewModel model)
        {
            Order order = new Order();
            order.Customer = _customerRepository.Retrieve(model.CustomerId!.Value);
            order.OrderDate = DateTime.Now;

            int count = 1;
            foreach(var item in model.SelectedItems!)
            {
                if (item.IsSelected)
                {
                    order.OrderItems!.Add(new OrderItem() {
                        Id = count,
                        Product = _productRepository.Retrieve(item.OrderItem!.Product!.Id),
                        Quantity = item.OrderItem.Quantity!,
                        PurchasePrice = item.OrderItem.PurchasePrice!
                    });
                    count++;
                }
            }

            _orderRepository.Save(order);

            return RedirectToAction("Index");
        }


    }
}
