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
            // Validação automática do modelo
            if (!ModelState.IsValid)
            {
                // Recarrega apenas a lista de clientes para manter os dados preenchidos pelo usuário
                model.Customers = _customerRepository.RetrieveAll();
                return View("Create", model);
            }

            // Monta o objeto Order
            var order = new Order
            {
                Customer = _customerRepository.Retrieve((int)model.CustomerId),
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            // Adiciona apenas os itens marcados (IsSelected)
            if (model.SelectedItems != null)
            {
                foreach (var item in model.SelectedItems.Where(x => x.IsSelected))
                {
                    if (item.OrderItem != null && item.OrderItem.Product != null)
                    {
                        order.OrderItems.Add(new OrderItem
                        {
                            Product = _productRepository.Retrieve(item.OrderItem.Product.Id),
                            Quantity = item.OrderItem.Quantity,
                            PurchasePrice = item.OrderItem.PurchasePrice
                        });
                    }
                }
            }

            // Salva o pedido na lista correta
            _orderRepository.Save(order);

            return RedirectToAction("Index");
        }


    }
}
