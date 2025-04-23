using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDBContext context) : IUnitOfWork
    {



        private readonly Dictionary<string,object> _Repositories = new Dictionary<string, object>();

        public IGenericReopsitory<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : ModelBase<Tkey>
        {
            var TypeName = typeof(TEntity).Name;


            if (_Repositories.ContainsKey(TypeName))
                return (IGenericReopsitory<TEntity, Tkey>)_Repositories[TypeName];




            var Repo=new GenericRepository<TEntity, Tkey>(context);
            _Repositories.Add(TypeName, Repo);
            return Repo;
        }



        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
