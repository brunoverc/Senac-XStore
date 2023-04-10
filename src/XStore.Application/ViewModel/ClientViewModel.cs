using System.ComponentModel.DataAnnotations;

namespace XStore.Application.ViewModel
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email { get; set; }        
        public string Cpf { get; set; }
        public bool Active { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
