using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository
    {
        public Product Retrieve(int id)
        {
            foreach(Product p in ProductData.Products)
            
                if(p.Id == id)
                
                    return p; 

            return null!; 
            
        }

        public List<Product> RetrieveByName(string name)
        {
            List<Product> ret = new List<Product>();
            foreach (Product p in ProductData.Products)

                if (p.ProductName!.ToLower().Contains(name.ToLower()))
                    ret.Add(p);
            return ret;
        }

        public List<Product> RetrieveAll()
        {
            return ProductData.Products;
        }

        public void Save(Product product)
        {
            product.Id = GetCount() + 1; // Atribui um novo ID baseado na contagem atual
            ProductData.Products.Add(product);
        }

        public bool Delete(Product product)
        {
            return ProductData.Products.Remove(product); // Remove o produto da lista de produtos
        }

        public bool DeleteById(int id) // Método para remover um produto pelo ID
        {
            Product product = Retrieve(id); // Recupera o produto pelo ID
            if (product != null) // Verifica se o produto foi encontrado

                return Delete(product); // Chama o método Delete para remover o produto
            return false; // Retorna false se o produto não foi encontrado
        }

        public void Update(Product product)
        {
            Product oldProduct = Retrieve(product.Id); // Recupera o produto antigo pelo ID
                oldProduct.ProductName = product.ProductName; // Atualiza o nome do produto
                oldProduct.Description = product.Description; // Atualiza a descrição do produto
                oldProduct.CurrentPrice = product.CurrentPrice; // Atualiza o preço atual do produto
        }

        public int GetCount()
        {
            return ProductData.Products.Count; // Retorna a contagem de produtos armazenados
        }
    }
}
