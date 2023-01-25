using XStore.Core.DomainObjects;

namespace XStore.Domain.Entities
{
    public class OrderItem : Entity
	{
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Amount { get; private set; }
        public decimal UnitValue { get; private set; }
        public string ProductImage { get; private set; }

        // EF Rel.
        public Order Order { get; private set; }

        // EF ctor
        protected OrderItem() { }

        public OrderItem(Guid orderId,
            Guid productId,
            string productName,
            int amount,
            decimal unitValue,
            string productImage)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            Amount = amount;
            UnitValue = unitValue;
            ProductImage = productImage;
        }

        internal decimal CalculeValue()
        {
            return Amount * UnitValue;
        }
    }
}

