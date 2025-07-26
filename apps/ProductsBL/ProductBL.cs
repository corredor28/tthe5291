using Microsoft.EntityFrameworkCore;
using ProductsDL;
using System.Text.Json.Serialization;

namespace ProductsBL
{
    public class ProductBL
    {
        private readonly ProductsdbContext _productsdbContext;
        public ProductBL(ProductsdbContext productsdbContext)
        {
            _productsdbContext = productsdbContext;
        }
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productsdbContext.Products.ToListAsync();
        }

        public async Task<Product> Get(int id)
        {
            var product = await _productsdbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            return product;
        }

        public async Task<Product> Add(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) )
                {
                    throw new ArgumentNullException(nameof(name), "Product cannot be null.");
                }
                var product = new Product { Name = name };
                _productsdbContext.Products.Add(product);
                await _productsdbContext.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BL An error occurred while adding the product: {ex.Message}");
                throw;
            }
        }

        public async Task<Product> Update(int id, string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentNullException(nameof(name), "Product cannot be null.");
                }


                var product = await _productsdbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                }
                product.Name = name;
                _productsdbContext.Update(product);
                await _productsdbContext.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BL An error occurred while updating the product: {ex.Message}");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var product = await _productsdbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                }
                _productsdbContext.Products.Remove(product);
                await _productsdbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BL An error occurred while deleting the product: {ex.Message}");
                throw;
            }
        }
    }
}
