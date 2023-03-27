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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(XStoreDbContext context) : base(context)
        {
        }

        public Product GetById(Guid id)
        {
            var context = DbSet.AsQueryable();
            var Product = context.FirstOrDefault(c => c.Id == id);
            return Product;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var context = DbSet.AsQueryable();
            var Product = await context.FirstOrDefaultAsync(c => c.Id == id);
            return Product;
        }

        public IEnumerable<Product> Search(Expression<Func<Product, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            return context.Where(predicate).ToList();
        }

        public async Task<IEnumerable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate)
        {
            var context = DbSet.AsQueryable();
            return await context.Where(predicate).ToListAsync();
        }

        public IEnumerable<Product> Search(Expression<Func<Product, bool>> predicate,
            int pageNumber,
            int pageSize)
        {
            var context = DbSet.AsQueryable();
            var result = context.Where(predicate).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return result;
        }

        public Product Add(Product entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public Product Update(Product entity)
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

        public void Remove(Expression<Func<Product, bool>> expression)
        {
            var context = DbSet.AsQueryable();
            var entities = context.Where(expression);
            DbSet.RemoveRange(entities);
        }
    }
}
