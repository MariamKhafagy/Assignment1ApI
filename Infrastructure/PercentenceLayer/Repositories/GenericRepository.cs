using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer.Repositories
{
    public class GenericRepository<TEntity, TKey> (StoreDbContext _dbContext)
        : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>  

    {
        public async Task AddAsync(TEntity entity) 
              => await _dbContext.Set<TEntity>().AddAsync(entity);


        public async Task<IEnumerable<TEntity>> GetALLASync()
              => await _dbContext.Set<TEntity>().ToListAsync();

        

        public async Task<TEntity?> GetBYIdAsync(TKey id )
             => await _dbContext.Set<TEntity>().FindAsync(id);

       

        public void Remove(TEntity entity)
             => _dbContext.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity)
             => _dbContext.Set<TEntity>().Update(entity);

        #region  With Specipfications
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set <TEntity>(), specifications ).ToListAsync();

        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).CountAsync();

        }

        public async Task<int> CountAsync(object countSpecs)
        {
            if (countSpecs is ISpecifications<TEntity, TKey> spec)
            {
                return await CountAsync(spec);  // call the main typed method
            }

            throw new ArgumentException(
                $"Expected {typeof(ISpecifications<TEntity, TKey>).Name}",
                nameof(countSpecs));
        }

        #endregion
    }
}
