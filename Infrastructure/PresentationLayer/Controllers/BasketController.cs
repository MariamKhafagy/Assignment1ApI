using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.DTOS.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager _serviceManager ) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string Key) {

         var basket=await  _serviceManager.BasketService.GEtBasketAsync(Key);
          return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var result = await _serviceManager.BasketService.CreateOrUpdateBasketASync(basket);
            return Ok(result);  

        }

        [HttpDelete]

        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        { 
          var res=await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(res);
        }

    }
}
