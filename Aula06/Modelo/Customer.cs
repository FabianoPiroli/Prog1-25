namespace Modelo
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? HomeAddress { get; set; }
        public string? WorkAddress { get; set; }

        public static int InstanceCount = 0;
        public int ObjectCount = 0;

        public bool Validate()
        {
            bool isValid = true;

            isValid =
                !string.IsNullOrEmpty(this.Name) &&
                (this.Id > 0) &&
                string.IsNullOrEmpty(this.HomeAddress);

            return isValid;
        }

        public Customer Retrieve()
        {
            return new Customer();
        }

        public void Save(Customer customer)
        {
            
        }
    }
}
