using Microsoft.AspNetCore.Mvc;
using Modelo;
using Repository;

namespace Aula05ClassesIdentificadas.Controllers
{
    public class OrderController : Controller
    {
        private OrderRepository _orderRepository;
        public OrderController()
        {
            _orderRepository = new OrderRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Order> orders = _orderRepository.RetrieveAll();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order o)
        {
            if (o.Validate())
            {
                _orderRepository.Save(o);
                List<Order> orders = _orderRepository.RetrieveAll();
                return View("Index", orders);
            }
            else
            {
                ModelState.AddModelError("", "Order is not valid.");
                return View(o);
            }
        }
    }
}
