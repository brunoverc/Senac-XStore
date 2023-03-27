using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XStore.Domain.Entities;
using XStore.Domain.Interfaces;
using XStore.Infra.Data.Context;

namespace XStore.Infra.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(XStoreDbContext context) : base(context)
        {
        }

        public Order GetById(Guid id)
        {
            var context = DbSet.AsQueryable();
            var Order = context.FirstOrDefault(c => c.Id == id);
            return Order;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            var context = DbSet.AsQueryable();
            var Order = await context.FirstOrDefaultAsync(c => c.Id == id);
            return Order;
        }

        public IEnumerable<Order> Search(Expression<Func<Order, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            return context.Where(predicate).ToList();
        }

        public async Task<IEnumerable<Order>> SearchAsync(Expression<Func<Order, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            return await context.Where(predicate).ToListAsync();
        }

        public IEnumerable<Order> Search(Expression<Func<Order, bool>> predicate,
            int pageNumber,
            int pageSize)
        {
            var context = DbSet.AsQueryable();
            var result = context.Where(predicate).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return result;
        }

        public Order Add(Order entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public async Task<Order> AddAsync(Order entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public Order Update(Order entity)
        {
            DbSet.Update(entity);
            return entity;
        }

        public void Remove(Guid id)
        {
            var obj = GetById(id);
            if (obj != null)
            {
                DbSet.Remove(obj);
            }
        }

        public void Remove(Expression<Func<Order, bool>> expression)
        {
            var context = DbSet.AsQueryable();
            var entities = context.Where(expression);
            DbSet.RemoveRange(entities);
        }
    }
}
