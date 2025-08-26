using Domain_Layer.Models;
using Domain_Layer.Repository_Interfaces;
using Persistence_Layer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence_Layer.Repositories
{
    public class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string,object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // Get Type Name
            var typeName = typeof(TEntity).Name;

            // Dic<string,Object> => key string (Name Of Type) , Object From Generic Repository
            if(_repositories.ContainsKey(typeName))
                return (IGenericRepository<TEntity,TKey>) _repositories[typeName];
            else
            {
                // Create Object
                var Repo = new GenericRepository<TEntity, TKey>(dbContext);
                // Store Object In Dic
                _repositories["typeName"] =  Repo;
                // Return Object
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
