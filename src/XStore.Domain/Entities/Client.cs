using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using XStore.Core.DomainObjects;

namespace XStore.Domain.Entities
{
    public class Client : Entity
    {
        protected Client() { }
        public Client(string name,
            Email email,
            Cpf cpf,
            bool active)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
            Active = active;
        }

        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Active { get; private set; }
        public Guid AddressId { get; private set; }
        public Address Address { get; private set; }

        public void SetAddress(Address address)
        {
            Address = address;
            AddressId = address.Id;
        }

        public void ChangeEmail(string email)
        {
            Email = new Email(email);
        }

    }
}
