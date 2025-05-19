namespace Modelo
{
    public class Order
    {
        public int Id { get; set; }
        public Customer? Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public string? ShippingAddress { get; set; }
        public List<OrderItem>? OrderItems { get; set; }

        public bool Validate()
        {
            bool isValid = true;

            isValid =
                (this.Id > 0) &&
                (this.Customer != null) &&
                (this.OrderDate != DateTime.MinValue) &&
                !string.IsNullOrEmpty(this.ShippingAddress);

            return isValid;
        }

        public Order Retrieve(int orderId)
        {
            return new Order();
        }

        public List<Order> Retrieve()
        {
            return new List<Order>();
        }

        public void Save(Order order)
        {

        }
    }
}
