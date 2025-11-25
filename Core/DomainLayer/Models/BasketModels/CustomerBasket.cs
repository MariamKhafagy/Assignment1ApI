using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.BasketModels
{
    public class CustomerBasket 
    {
        public string Id { get; set; }//GUId : Created from Client [front-End]
        public ICollection<BasketItem> Items { get; set; }

    }
}
