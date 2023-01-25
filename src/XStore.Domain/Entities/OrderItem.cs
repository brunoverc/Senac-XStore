using XStore.Core.DomainObjects;

namespace XStore.Domain.Entities
{
    public class OrderItem : Entity
	{
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProdutoName { get; private set; }
        public int Amount { get; private set; }
        public decimal UnitValue { get; private set; }
        public string ProductImage { get; set; }

        // EF Rel.
        public Order Order { get; set; }

        // EF ctor
        protected OrderItem() { }

        public OrderItem(Guid orderId,
            Guid productId,
            string produtoName,
            int amount,
            decimal unitValue,
            string productImage)
        {
            OrderId = orderId;
            ProductId = productId;
            ProdutoName = produtoName;
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

