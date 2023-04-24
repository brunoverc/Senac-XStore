using System.ComponentModel.DataAnnotations;

namespace XStore.Application.ViewModel
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        [Required(ErrorMessage = "O campo Preço é obrigatório, mesmo que seja zero")]
        public decimal Price { get; set; }
        public DateTime DateRegister { get; set; }
        public string Image { get; set; }
        public int StockQuantity { get; set; }
    }
}
