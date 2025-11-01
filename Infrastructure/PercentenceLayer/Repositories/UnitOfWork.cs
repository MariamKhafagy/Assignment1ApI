using DomainLayer.Contracts;
using DomainLayer.Models;
using PersistenceLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repository = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           // Get Type Name
           var typeName=typeof(TEntity).Name;
            //Dic<string>,Objecr ,, Object from GereicRepository<Product>


            //if (_repository.ContainsKey(typeName))
            //    return (IGenericRepository<TEntity, TKey>)_repository[typeName];

            if (_repository.TryGetValue(typeName,out object? value))
                return (IGenericRepository<TEntity, TKey>)value;
            else
            {
                //Creating object
                var repo = new GenericRepository<TEntity,TKey>(_dbContext);
               // _repository[typeName] = repo; //Store Reo In Dictionary
                _repository.Add(typeName, repo);
                return repo;
            }

        }

        public async Task<int> SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
    }
}
