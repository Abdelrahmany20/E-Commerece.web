using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Dto_s.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation.Controllers
{


    public class BasketController(IServicesManger servicesManger) :ApiBaseController
    {




        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var Basket  = await servicesManger.BasketServices.GetBasketAsync(key);
            return Ok(Basket);
        }





        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await servicesManger.BasketServices.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }



        [HttpDelete("{Key}")]

        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var Basket = await servicesManger.BasketServices.DeleteBasketAsync(key);
            return Ok(Basket);
        }

    }
}
