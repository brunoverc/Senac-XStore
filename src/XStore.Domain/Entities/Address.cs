using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XStore.Core.DomainObjects;

namespace XStore.Domain.Entities
{
    public class Address : Entity
    {
        //EF Contructor
        protected Address() { }
        public Address(string street,
            string number,
            string complement,
            string neighborhood,
            string postalCode,
            string city,
            string state)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            PostalCode = postalCode;
            City = city;
            State = state;
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        
    }
}
