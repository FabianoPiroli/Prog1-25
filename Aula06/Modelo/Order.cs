namespace Modelo
{
    public class Order
    {
        #region Atributos
        public int Id { get; set; }
        public Customer? Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public Address? ShippingAddress { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        #endregion

        #region Construtor
        public Order()
        {
            OrderDate = DateTime.Now;
            OrderItems = new List<OrderItem>();
        }

        public Order(int orderId) : this() //Cria outro construtor com sobrecarga de Order
                                           // this chama o construtor padrão para herdar os atributos
        {
            this.Id = orderId;
        }
        #endregion

        public bool Validate()
        {
            bool isValid = true;

            isValid =
                (this.Id > 0) &&
                (this.Customer != null) &&
                (this.OrderDate != DateTime.MinValue) &&
                (this.ShippingAddress != null);

            return isValid;
        }

        public Order Retrieve(int orderId)
        {
            return new Order();
        }
    }
}
