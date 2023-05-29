using XStore.Core.DomainObjects;
using XStore.Core.Enums;

namespace XStore.Domain.Entities
{
    public class Order : Entity
    {
        public Guid ClientId { get; private set; }
        public decimal TotalValue { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public string Code { get; private set; }
        public Guid AddressId { get; private set; }

        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        // EF Rel.
        public Address Address { get; private set; }
        public Voucher Voucher { get; private set; }

        protected Order() { }

        public Order(Guid clientId,
            decimal totalValue,
            List<OrderItem> orderItems,
            bool voucherUsed,
            decimal discount = 0,
            Guid? voucherId = null)
        {
            ClientId = clientId;
            TotalValue = totalValue;
            _orderItems = orderItems;
            VoucherId = voucherId;
            VoucherUsed = voucherUsed;
            Discount = discount;
        }

        public void AuthorizeOrder()
        {
            OrderStatus = OrderStatus.Autorizado;
        }
        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Cancelado;
        }

        public void FinalizeOrder()
        {
            OrderStatus = OrderStatus.Pago;
        }

        public void SetVoucher(Voucher voucher)
        {
            VoucherId = voucher.Id;
            Voucher = voucher;

            CalculateValueTotalDiscount();
        }

        public void SetAddress(Address address)
        {
            Address = address;
            AddressId = address.Id;
        }

        public void CalculateOrderValue()
        {
            if(OrderItems == null)
            {
                TotalValue = 0;
            }
            else
            {
                TotalValue = OrderItems.Sum(p => p.CalculeValue());
            }
            
            CalculateValueTotalDiscount();
        }

        public void CalculateValueTotalDiscount()
        {
            decimal discount = 0;
            var value = TotalValue;

            if (Voucher.DiscountType == DiscountTypeVoucher.Porcentagem)
            {
                if (Voucher.Percentage.HasValue)
                {
                    discount = (value * Voucher.Percentage.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if (Voucher.DiscountValue.HasValue)
                {
                    discount = Voucher.DiscountValue.Value;
                    value -= discount;
                }
            }

            if (value < 0)
            {
                TotalValue = 0;
            }
            else
            {
                TotalValue = value;
            }

            Discount = discount;
        }

        public void SetCode()
        {
            Code = SetRandomAlphanumeric(4);
        }
        private string SetRandomAlphanumeric (int size)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, size)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public void SetStatus(OrderStatus status)
        {
            OrderStatus = status;
        }
    }

}

