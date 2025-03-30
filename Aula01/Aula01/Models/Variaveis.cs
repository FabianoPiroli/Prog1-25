namespace Aula01.Models
{
    public class Variaveis
    {
        public byte MinValue = 0;
        public byte MaxValue = 255;
        public short MinShortValue = -32768;
        public short MaxShortValue = 32767;
        public int MinIntValue = -2147483648;
        public int MaxIntValue = 2147483647;
        public uint MinUIntValue = 0;
        public uint MaxUIntValue = 4294967295;
        public long MinLongValue = -9223372036854775808;
        public long MaxLongValue = 9223372036854775807;

        // Tipos Implícitos
        // var teste = 10;

        // Anotação de tipos
        public int userCount = 10;

        // Uma variável pode ser declara e não inicializada
        public int totalCount;

        // CONSTANTES
        // Para declarar uma constante, utilizamos a palavra reservada CONST
        // No entanto a CONST deve ser inicializada no momento da declaração
        public const int interestRate = 10;

        // O método construtor é invocado na instanciação do objeto por meio da palavra reservada new
        // Por regra, o construtor sempre tem o mesmo nome da classe
        public Variaveis()
        {
            totalCount = 0;

            // Tipo Implícito
            // A palavra chave var se encarrega de definir o tipo da variável na instrução de atribuição
            var signalStrength = 22;
            var companyName = "ACME";
        }
    }
}
