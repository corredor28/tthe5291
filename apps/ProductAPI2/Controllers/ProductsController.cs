using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsBL;
using ProductsDL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductAPI2.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsdbContext _productsdbContext;
        private readonly ProductBL _productBL;
        public ProductsController(ProductsdbContext productsdbContext)
        {
            _productsdbContext = productsdbContext;
            _productBL = new ProductBL(_productsdbContext);
        }

        // GET: api/products
        [HttpGet()]
        public async Task<IEnumerable<Product>> Get()
        {
            var products = await _productBL.Get();
            return products;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            var product = await _productBL.Get(id);
            return product;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task Post([FromBody] string name)
        {
            await _productBL.Add(name);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] string name)
        {
            await _productBL.Update(id, name);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                await _productBL.Delete(id);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the product: {ex.Message}");
                throw;
            }
        }
    }
}
