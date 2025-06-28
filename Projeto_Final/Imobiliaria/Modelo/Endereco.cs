using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Endereco
    {
        public int Id { get; set; }
        public string? Logradouro { get; set; }
        public string? Referencia { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? CEP { get; set; }
        public string? Pais { get; set; }
        public string? TipoEndereco { get; set; }

        public bool Validate()
        {
            bool isValid = true;

            isValid =
                !string.IsNullOrEmpty(this.Logradouro) &&
                !string.IsNullOrEmpty(this.Referencia) &&
                !string.IsNullOrEmpty(this.Cidade) &&
                !string.IsNullOrEmpty(this.Estado) &&
                !string.IsNullOrEmpty(this.CEP) &&
                !string.IsNullOrEmpty(this.Pais) &&
                !string.IsNullOrEmpty(this.TipoEndereco);




            return isValid;
        }

    }
}
