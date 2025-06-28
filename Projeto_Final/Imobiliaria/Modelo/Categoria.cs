namespace Modelo
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Imovel> Imoveis { get; set; }

        public bool Validate()
        {
            bool isValid = true;
            isValid =
                !string.IsNullOrEmpty(this.Nome) &&
                (this.Id > 0);

            return isValid;
        }
    }
}
