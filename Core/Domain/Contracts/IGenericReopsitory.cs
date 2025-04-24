using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericReopsitory<TEntity,TKey> where TEntity : ModelBase<TKey>
    {

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id); 
        
        
        
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> spec);
        Task<TEntity> GetByIdAsync(ISpecifications<TEntity, TKey> spec);


        Task<int> CountAsync(ISpecifications<TEntity, TKey> spec);




        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
    
    
}
