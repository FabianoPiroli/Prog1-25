using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Vacina
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime? DtAplicacao { get; set; }
        public Animais? IdAnimal { get; set; }

        public bool Validade()
        {
            bool isValid = true;
            isValid =
                !string.IsNullOrEmpty(Nome) &&
                (this.Id > 0) &&
                (this.DtAplicacao != DateTime.MinValue) &&
                (this.IdAnimal != null);
            return isValid;
        }
    }
}
