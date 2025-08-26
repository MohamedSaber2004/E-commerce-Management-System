using Shared.DataTransferObjects.AuthenticationModuleDTos;

namespace Shared.DataTransferObjects.OrderModuleDTos
{
    public class OrderToReturnDTo
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }
        public AddressDTo ShipToAddress { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public decimal DeliveryCost { get; set; }
        public string Status { get; set; } = default!;
        public ICollection<OrderItemDTo> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
