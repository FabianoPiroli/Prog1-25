using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderData
    {
        // Listas para armazenar os dados dos clientes, produtos e pedidos
        public static List<Customer> Customers { get; set; } = [];
        public static List<Product> Products { get; set; } = [];
        public static List<Order> Orders { get; set; } = new();
    }
}
