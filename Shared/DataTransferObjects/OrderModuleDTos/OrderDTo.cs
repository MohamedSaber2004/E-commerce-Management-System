using Shared.DataTransferObjects.AuthenticationModuleDTos;

namespace Shared.DataTransferObjects.OrderModuleDTos
{
    public class OrderDTo
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethodId { get; set; } = default!;
        public AddressDTo ShipToAddress { get; set; } = default!;
    }
}
