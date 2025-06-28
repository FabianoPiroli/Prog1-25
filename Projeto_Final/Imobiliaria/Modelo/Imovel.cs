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
        public Endereco Endereco { get; set; } = new Endereco();
        public string Descricao { get; set; }
        public string CaminhoImagem { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public bool Validate()
        {
            bool isValid = true;

            isValid =
                (this.Id > 0) &&
                !string.IsNullOrEmpty(this.Nome) &&
                (this.Preco > 0) &&
                (this.Quartos > 0) &&
                (this.Banheiros > 0) &&
                (this.VagasGaragem > 0) &&
                (this.Endereco != null) &&
                !string.IsNullOrEmpty(this.Descricao) &&
                (this.CategoriaId > 0) &&
                (this.Categoria != null);

            return isValid;
        }
    }
}
