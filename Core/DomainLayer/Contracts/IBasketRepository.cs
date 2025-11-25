using DomainLayer.Models.BasketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketASync(string Key);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket ,TimeSpan? TomeoLive=null );
        Task<bool>DeleteBasketAsync(string Key);

    }
}
