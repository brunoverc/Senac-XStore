using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XStore.Core.DomainObjects;

namespace XStore.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        long Count();
        long Count(Expression<Func<T, bool>> predicate);
    }
}
