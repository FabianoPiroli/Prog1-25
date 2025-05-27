using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CustomerRepository
    {
        public Customer Retrieve(int id)
        {
            foreach(Customer c in CustomerData.Customers)
            
                if(c.Id == id) // Verifica se o ID do cliente corresponde ao ID procurado

                    return c;
                
            return null!; // Retorna null se não encontrar o cliente

        }

        public List<Customer> RetrieveByName(string name)
        {
            List<Customer> ret = new List<Customer>(); // Cria uma lista para armazenar os clientes encontrados

            foreach (Customer c in CustomerData.Customers) // Percorre todos os clientes armazenados
            
                if (c.Name!.ToLower().Contains(name.ToLower())) // Verifica se o nome do cliente contém a string procurada
                    ret.Add(c); // Adiciona o cliente à lista se o nome contiver a string procurada

            return ret; // Retorna a lista de clientes encontrados
        }

        public List<Customer> RetrieveAll() // Método para recuperar todos os clientes
        {
            return CustomerData.Customers; // Retorna a lista de todos os clientes armazenados
        }

        public void Save(Customer customer)
        {
            customer.Id = GetCount() + 1; // Atribui um novo ID baseado na contagem atual
            CustomerData.Customers.Add(customer);
        }
        
        public bool Delete(Customer customer)
        {
            return CustomerData.Customers.Remove(customer); // Remove o cliente da lista de clientes
        }

        public bool DeletById(int id) // Método para remover um cliente pelo ID
        {
            Customer customer = Retrieve(id); // Recupera o cliente pelo ID

            if (customer != null) // Verifica se o cliente foi encontrado
            
                return Delete(customer); // Chama o método Delete para remover o cliente

            return false; // Retorna false se o cliente não foi encontrado
        }

        public void Update(Customer newCustomer)
        {
            Customer oldCustomer = Retrieve(newCustomer.Id); // Recupera o cliente antigo pelo ID
            oldCustomer.Name = newCustomer.Name; // Atualiza o nome do cliente antigo com o novo nome
            oldCustomer.WorkAddress = newCustomer.WorkAddress; // Atualiza o endereço de trabalho do cliente antigo com o novo endereço
            oldCustomer.HomeAddress = newCustomer.HomeAddress; // Atualiza o endereço residencial do cliente antigo com o novo endereço
        }

        public int GetCount()
        {
            return CustomerData.Customers.Count; // Retorna a contagem de clientes armazenados
        }
    }
}
