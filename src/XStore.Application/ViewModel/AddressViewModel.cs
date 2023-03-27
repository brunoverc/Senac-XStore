using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XStore.Application.ViewModel
{
    public class AddressViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo Rua é obrigatório")]
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        [Required(ErrorMessage = "O campo Bairro é obrigatório")]
        public string Neighborhood { get; set; }
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "O campo Cidade é obrigatório")]
        public string City { get; set; }
        [Required(ErrorMessage = "O campo Estado é obrigatório")]
        public string State { get; set; }
    }
}
