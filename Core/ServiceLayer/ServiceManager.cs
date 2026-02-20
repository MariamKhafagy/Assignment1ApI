using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using ServiceAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ServiceManager(IUnitOfWork _unitOfWork, 
                                IMapper _mapper, 
                                IBasketRepository _basketRepository,
                                UserManager<ApplicationUser> userManager): IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService
                                = new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper) );
        public IProductService ProductService => _LazyProductService.Value;

        private readonly Lazy<IBasketService> _LazyBasketService
                               = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        public IBasketService BasketService => _LazyBasketService.Value;


        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService
                             = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager));
        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;


    }
}
