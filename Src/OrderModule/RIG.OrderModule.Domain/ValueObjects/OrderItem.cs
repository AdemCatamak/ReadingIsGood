using System.Collections.Generic;
using RIG.Shared.Domain;

namespace RIG.OrderModule.Domain.ValueObjects
{
    public class OrderItem : ValueObject
    {
        public string ProductId { get; private set; }
        public int Quantity { get; private set; }

        public OrderItem(string productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ProductId;
            yield return Quantity;
        }
    }
}