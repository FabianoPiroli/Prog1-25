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
        public Order Retrieve(int id)
        {
            foreach (Order o in CustomerData.Orders)

                if (o.Id == id) // Verifica se o ID do cliente corresponde ao ID procurado

                    return o;

            return null!; // Retorna null se não encontrar o cliente

        }

        public List<Order> RetrieveByName(string name)
        {
            List<Order> ret = []; // Cria uma lista para armazenar os clientes encontrados

            foreach (Order o in CustomerData.Orders) // Percorre todos os clientes armazenados

                if (o.Customer!.Name!.ToLower().Contains(name.ToLower())) // Verifica se o nome do cliente contém a string procurada
                    ret.Add(o); // Adiciona o cliente à lista se o nome contiver a string procurada

            return ret; // Retorna a lista de clientes encontrados
        }

        public List<Order> RetrieveAll() // Método para recuperar todos os clientes
        {
            return OrderData.Orders; // Retorna a lista de todos os clientes armazenados
        }

        public void Save(Order order)
        {
            order.Id = GetCount() + 1; // Atribui um novo ID baseado na contagem atual
            OrderData.Orders.Add(order);
        }

        public bool Delete(Order order)
        {
            return CustomerData.Orders.Remove(order); // Remove o cliente da lista de clientes
        }

        public bool DeletById(int id) // Método para remover um cliente pelo ID
        {
            Order order = Retrieve(id); // Recupera o cliente pelo ID

            if (order != null) // Verifica se o cliente foi encontrado

                return Delete(order); // Chama o método Delete para remover o cliente

            return false; // Retorna false se o cliente não foi encontrado
        }

        public void Update(Order newOrder)
        {
            Order oldOrder = Retrieve(newOrder.Id); // Recupera o cliente antigo pelo ID
            oldOrder.Customer = newOrder.Customer; // Atualiza o cliente
            oldOrder.OrderDate = newOrder.OrderDate; // Atualiza a data do pedido
            oldOrder.ShippingAddress = newOrder.ShippingAddress; // Atualiza o endereço de entrega
            oldOrder.OrderItems = newOrder.OrderItems; // Atualiza os itens do pedido
        }

        public int GetCount() => CustomerData.Orders.Count; // Método para obter a contagem de pedidos

    }
}
