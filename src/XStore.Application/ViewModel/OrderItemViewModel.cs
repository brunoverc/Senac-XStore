namespace XStore.Application.ViewModel
{
    public class OrderItemViewModel
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public OrderViewModel? Order { get; set; }
        public ProductViewModel? Product { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
    }
}
