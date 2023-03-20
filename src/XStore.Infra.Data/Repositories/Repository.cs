using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XStore.Core.DomainObjects;
using XStore.Domain.Entities;
using XStore.Domain.Interfaces;
using XStore.Infra.Data.Context;

namespace XStore.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly XStoreDbContext Db;
        protected readonly DbSet<T> DbSet;

        public Repository(XStoreDbContext context)
        {
            Db = context;
            DbSet = Db.Set<T>();
        }

        protected IQueryable<T> Query()
        {
            return DbSet;
        }

        public void Dispose()
        {
            Db?.Dispose();
            GC.SuppressFinalize(this);
        }

        public long Count()
        {
            return DbSet.LongCount();
        }

        public long Count(Expression<Func<T, bool>> predicate)
        {
            var result = DbSet.LongCount(predicate);
            return result;
        }

        
    }
}
