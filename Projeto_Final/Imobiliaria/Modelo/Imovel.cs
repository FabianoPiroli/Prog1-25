namespace Modelo
{
    public class Imovel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quartos { get; set; }
        public int Banheiros { get; set; }
        public int VagasGaragem { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
        public string CaminhoImagem { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
