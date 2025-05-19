namespace Modelo
{
    public class Animais
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? DtNascimento{ get; set; }
        public string? Cor { get; set; }
        public string? Sexo { get; set; }
        public string? Peso { get; set; }
        public string? Altura { get; set; }

        public bool Validade()
        {
            return true;
        }

        public Animais Retrieve()
        {
            return new Animais();
        }

        public void Save(Animais animais)
        {

        }
    }
}
