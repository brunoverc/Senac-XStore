using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XStore.Core.DomainObjects
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = new Guid();
        }

        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
