using Abstraction;
using AutoMapper;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public class ServicesManger(IUnitOfWork unitOfWork,IMapper mapper) : IServicesManger
    {
        private readonly Lazy<IProductServices> _LazyproductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper)); 

        public IProductServices ProductServices => _LazyproductServices.Value;
    }
}
