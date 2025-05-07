using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exeptions;
using Domain.Models.Basket;
using Shared.Dto_s.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketServices(IBasketRepository Repo, IMapper mapper) : IBasketServices
    {



        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket = await Repo.GetBasketAsync(Key);
            if (Basket is not null)

                return mapper.Map<CustomerBasket, BasketDto>(Basket);

            else
                throw new BasketNotFoundException(Key); 


        }

        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = mapper.Map<BasketDto, CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await Repo.CreateOrUpdateBasketAsync(CustomerBasket);


            if (CreatedOrUpdatedBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Basket Not Created or Updated");



        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
           return await Repo.DeleteBasketAsync(Key);
        }

    }
}
