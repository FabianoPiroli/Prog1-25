using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Address
    {
        public int Id { get; set; }
        public string? StreetLine1 { get; set; }
        public string? StreetLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? AddressType { get; set; }

        public bool Validate()
        {
            bool isValid = true;

            isValid =
                !string.IsNullOrEmpty(this.StreetLine1) &&
                !string.IsNullOrEmpty(this.StreetLine2) &&
                !string.IsNullOrEmpty(this.City) &&
                !string.IsNullOrEmpty(this.State) &&
                !string.IsNullOrEmpty(this.PostalCode) &&
                !string.IsNullOrEmpty(this.Country) &&
                !string.IsNullOrEmpty(this.AddressType);
                
                
               

            return isValid;
        }

    }
}
