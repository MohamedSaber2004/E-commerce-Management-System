using System.ComponentModel.DataAnnotations.Schema;

namespace Domain_Layer.Models.OrderModule
{
    public class Order:BaseEntity<Guid>
    {
        public Order() { }

        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = userEmail;
            ShipToAddress = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; } = default!;
        public OrderAddress ShipToAddress { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public int DeliveryMethodId { get; set; }
        public OrderStatus Status { get; set; }

        //[NotMapped]
        //public decimal Total => SubTotal + DeliveryMethod.Price;
        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;
        public string PaymentIntentId { get; set; }
    }
}
