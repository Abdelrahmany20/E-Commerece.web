using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public class ServicesManger(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository basketRepository, UserManager<ApplicationUser> userManager) : IServicesManger
    {
        private readonly Lazy<IProductServices> _LazyproductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper)); 

        public IProductServices ProductServices => _LazyproductServices.Value;


        private readonly Lazy<IBasketServices> _lazyBasker = new Lazy<IBasketServices>(() => new BasketServices(basketRepository, mapper));


        public IBasketServices BasketServices => _lazyBasker.Value;



        private readonly Lazy<IAuthenticationServices> _lazyAuth = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager));

        public IAuthenticationServices AuthenticationServices => _lazyAuth.Value;
    }
}
 