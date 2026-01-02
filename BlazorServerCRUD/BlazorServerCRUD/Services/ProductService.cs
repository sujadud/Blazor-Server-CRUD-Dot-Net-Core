using BlazorServerCRUD.Data;
using BlazorServerCRUD.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerCRUD.Services
{
    public class ProductService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public ProductService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> UpdateProductAsync(Product updateProduct)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var existProduct = await context.Products
                .FirstOrDefaultAsync(x => x.Id == updateProduct.Id);

            if(existProduct is not null)
            {
                existProduct.Name = updateProduct.Name;
                existProduct.Price = updateProduct.Price;
                await context.SaveChangesAsync();
                return existProduct;
            }
            return null;
        }

        public async Task DeleteProductAsync(int id)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var existingProduct = await context.Products
                .FirstOrDefaultAsync(x => x.Id == id);
            if(existingProduct is not null)
            {
                context.Products.Remove(existingProduct);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }
    }
}
