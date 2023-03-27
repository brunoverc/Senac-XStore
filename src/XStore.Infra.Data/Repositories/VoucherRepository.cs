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
    public class VoucherRepository : Repository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(XStoreDbContext context) : base(context)
        {
        }

        public Voucher GetById(Guid id)
        {
            var context = DbSet.AsQueryable();
            var Voucher = context.FirstOrDefault(c => c.Id == id);
            return Voucher;
        }

        public async Task<Voucher> GetByIdAsync(Guid id)
        {
            var context = DbSet.AsQueryable();
            var Voucher = await context.FirstOrDefaultAsync(c => c.Id == id);
            return Voucher;
        }

        public IEnumerable<Voucher> Search(Expression<Func<Voucher, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            return context.Where(predicate).ToList();
        }

        public async Task<IEnumerable<Voucher>> SearchAsync(Expression<Func<Voucher, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            return await context.Where(predicate).ToListAsync();
        }

        public IEnumerable<Voucher> Search(Expression<Func<Voucher, bool>> predicate,
            int pageNumber,
            int pageSize)
        {
            var context = DbSet.AsQueryable();
            var result = context.Where(predicate).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return result;
        }

        public Voucher Add(Voucher entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public async Task<Voucher> AddAsync(Voucher entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public Voucher Update(Voucher entity)
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

        public void Remove(Expression<Func<Voucher, bool>> expression)
        {
            var context = DbSet.AsQueryable();
            var entities = context.Where(expression);
            DbSet.RemoveRange(entities);
        }
    }
}
