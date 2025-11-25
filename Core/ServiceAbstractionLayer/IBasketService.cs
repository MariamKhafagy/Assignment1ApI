using Shared.DTOS.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
    public interface IBasketService
    {
        Task<BasketDto> GEtBasketAsync(string Key);
        Task<BasketDto> CreateOrUpdateBasketASync(BasketDto basket);
        Task<bool> DeleteBasketAsync(string key);
    }
}
