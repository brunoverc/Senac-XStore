using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XStore.Domain.Entities;
using XStore.Domain.Interfaces;
using XStore.Infra.Data.Context;

namespace XStore.Infra.Data.Repositories
{
    /// <summary>
    /// Herdei da classe Repository mas agora passando a classe específica Addres
    /// Ao invés de passar uma classe genérica do tipo T
    /// Falei que essa minha classe é a implementação de IAddressRepository(Interface)
    /// </summary>
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        /// <summary>
        /// Criei um construtor que recebe um DbContext e passa ele para a classe
        /// pai, pois é a classe pai que sabe o que fazer com esse context
        /// </summary>
        /// <param name="context"></param>
        public AddressRepository(XStoreDbContext context) : base(context)
        {
        }

        public Address GetById(Guid id)
        {
            var context = DbSet.AsQueryable();
            var address = context.FirstOrDefault(x => x.Id == id);
            return address;
        }

        public async Task<Address> GetByIdAsync(Guid id)
        {
            var context = DbSet.AsQueryable();
            var address = await context.FirstOrDefaultAsync(x => x.Id == id);
            return address;
        }

        public IEnumerable<Address> Search(Expression<Func<Address, bool>> predicate)
        {
            var context = DbSet.AsQueryable();

            return context.Where(predicate).ToList();
        }

        public async Task<IEnumerable<Address>> SearchAsync(Expression<Func<Address, bool>> predicate)
        {
            var context = DbSet.AsQueryable();

            return await context.Where(predicate).ToListAsync();
        }

        public IEnumerable<Address> Search(Expression<Func<Address, bool>> predicate,
            int pageNumber,
            int pageSize)
        {
            var context = DbSet.AsQueryable();
            var result = context.Where(predicate).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return result;
        }

        public Address Add(Address entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public async Task<Address> AddAsync(Address entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public Address Update(Address entity)
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

        public void Remove(Expression<Func<Address, bool>> expression)
        {
            var context = DbSet.AsQueryable();
            var entities = context.Where(expression);
            DbSet.RemoveRange(entities);
        }
    }
}
