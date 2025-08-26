﻿
namespace Shared.DataTransferObjects.BasketModuleDTos
{
    public class BasketDTo
    {
        public string Id { get; set; }
        public ICollection<BasketItemDTo> Items { get; set; } = [];
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
