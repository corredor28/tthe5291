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

        public async Task<Product> Add(Product product)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(product.Name) )
                {
                    throw new ArgumentNullException(nameof(product.Name), "Product cannot be null.");
                }
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

        public async Task<Product> Update(int id, Product product)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(product.Name))
                {
                    throw new ArgumentNullException(nameof(product.Name), "Product cannot be null.");
                }

                var productDB = await _productsdbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (productDB == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                }

                productDB.Name = product.Name;
                productDB.Price = product.Price;
                _productsdbContext.Update(productDB);
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
