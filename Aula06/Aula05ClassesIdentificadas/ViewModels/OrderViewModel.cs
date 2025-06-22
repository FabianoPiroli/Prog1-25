using Modelo;

namespace Aula05ClassesIdentificadas.ViewModels
{
    public class OrderViewModel
    {
        public List<Customer> Customers { get; set; } = []; 
        public int? CustomerId { get; set; }
        public List<SelectedItem>? SelectedItems { get; set; } // Lista de itens selecionados
    }

    public class SelectedItem // Classe para representar um item selecionado na ViewModel
    {
        public bool IsSelected { get; set; } = false; // Faz com que o item não esteja selecionado por padrão
        public OrderItem? OrderItem { get; set; } = null!; // Inicializa como null para evitar erros de referência nula

    }
}
