namespace Modelo
{
    public class Animais
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? DtNascimento{ get; set; }
        public string? Cor { get; set; }
        public string? Sexo { get; set; }
        public float? Peso { get; set; }
        public float? Altura { get; set; }

        public bool Validade()
        {
            bool isValid = true;
            isValid = 
                !string.IsNullOrEmpty(Nome) &&
                (this.Id > 0) &&
                (this.DtNascimento != DateTime.MinValue) &&
                !string.IsNullOrEmpty(Cor) &&
                !string.IsNullOrEmpty(Sexo) &&
                (this.Peso > 0) &&
                (this.Altura > 0);
            return isValid;
        }
    }
}
