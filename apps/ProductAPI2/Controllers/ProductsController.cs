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
        public async Task<IActionResult> Get()
        {
            var products = await _productBL.Get();
            return Ok(products);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productBL.Get(id);
            return Ok(product);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            await _productBL.Add(product);
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            await _productBL.Update(id, product);
            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productBL.Delete(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
