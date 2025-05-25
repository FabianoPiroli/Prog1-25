using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class TipoAnimal
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        public bool Validade()
        {
            bool isValid = true;
            isValid =
                !string.IsNullOrEmpty(Nome) &&
                (this.Id > 0);
            return isValid;
        }
    }
}
