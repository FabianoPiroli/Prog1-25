using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository
    {
        public List<Order> Retrieve(int id)
        {
            foreach (Order o in OrderData.Orders)
            {
                if (o.Id == id) // Verifica se o ID do pedido corresponde ao ID procurado
                {
                    return new List<Order> { o }; // Retorna o pedido encontrado em uma lista
                }
            }
            return null!; // Retorna null se não encontrar o pedido
        }

        public List<Order> RetrieveByCustomerId(int customerId)
        {
            List<Order> orders = new List<Order>();
            foreach (Order o in OrderData.Orders)
            {
                if (o.Customer != null && o.Customer.Id == customerId) // Verifica se o pedido pertence ao cliente
                {
                    orders.Add(o); // Adiciona o pedido à lista de pedidos do cliente
                }
            }
            return orders; // Retorna a lista de pedidos do cliente
        }

        public List<Order> RetrieveAll()
        {
            return OrderData.Orders; // Retorna todos os pedidos
        }

        public void Save(Order order)
        {
            order.Id = GetCount() + 1; // Atribui um novo ID baseado na contagem atual
            OrderData.Orders.Add(order);
        }

        public bool Delete(Order order)
        {
            return OrderData.Orders.Remove(order); // Remove o pedido da lista de pedidos
        }

        public bool DeleteById(int id) // Método para remover um pedido pelo ID
        {
            Order order = Retrieve(id).FirstOrDefault(); // Recupera o pedido pelo ID
            if (order != null) // Verifica se o pedido foi encontrado
            {
                return Delete(order); // Chama o método Delete para remover o pedido
            }
            return false; // Retorna false se o pedido não foi encontrado
        }

        public void Udate(Order order)
        {
            // Atualiza o pedido na lista de pedidos
            for (int i = 0; i < OrderData.Orders.Count; i++)
            {
                if (OrderData.Orders[i].Id == order.Id)
                {
                    OrderData.Orders[i] = order; // Substitui o pedido existente pelo novo
                    break;
                }
            }
        }

        public int GetCount()
        {
            return OrderData.Orders.Count; // Retorna a contagem de pedidos
        }
    }
}
