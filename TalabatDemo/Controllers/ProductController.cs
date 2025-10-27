using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatDemo.Models;

namespace TalabatDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase //BaseUrl /api/Product
    {

        [HttpGet("{id}")]  // Get :BaseUrl /api/Product?id=6
        public ActionResult<Product> Get(int id)
        {
            return new Product() { Id = id };
        
        }

        [HttpGet]  // Get :BaseUrl /api/Product
        public ActionResult<Product> GetAll()
        { 
            return new Product() { Id = 20 };

        }

        [HttpPost]

        public ActionResult<Product> AddProduct(Product product)
        {
            return new Product();
        }

        [HttpPost("brand")]

        public ActionResult<Product> AddBrand(Product product)
        {
            return new Product();
        }



        [HttpPut]

        public ActionResult<Product> UpdateProduct(Product product)
        {
            return new Product();
        }

        [HttpDelete]

        public ActionResult<Product> DeleteProduct(Product product)
        {
            return new Product();
        }

    }
}
