using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModels;
using ServiceAbstractionLayer;
using Shared.DTOS.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class BasketService(IBasketRepository _basketRepository , IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketASync(BasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basket);
            var createdorUpdatedBasket= await _basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            if (createdorUpdatedBasket is not null) return await GEtBasketAsync(basket.Id);
            else throw new Exception("Can Not Update Or Create Basket , Try again later ");
        }

        public async Task<bool> DeleteBasketAsync(string key)
          => await _basketRepository.DeleteBasketAsync(key);

        public async Task<BasketDto> GEtBasketAsync(string Key)
        {
            var basket=await _basketRepository.GetBasketASync(Key);
            if (basket is not null) return _mapper.Map<BasketDto>(basket);
            else throw new BasketNotFoundException(Key);
        }
    }
}
