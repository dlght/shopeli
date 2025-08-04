using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Shopeli.ProductManagement.API.Models;

namespace Shopeli.ProductManagement.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IDynamoDBContext _context;

        public ProductService(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Brand = request.Brand,
                Unit = request.Unit,
                Quantity = request.Quantity
            };

            await _context.SaveAsync(product);
            return product;
        }

        public async Task<Product?> GetProductAsync(string id)
        {
            return await _context.LoadAsync<Product>(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var conditions = new List<ScanCondition>();
            return await _context.ScanAsync<Product>(conditions).GetRemainingAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByDateAsync(DateTime date)
        {
            var startDate = date.Date;
            var endDate = date.Date.AddDays(1);

            var conditions = new List<ScanCondition>
            {
                new ScanCondition("CreatedDate", ScanOperator.GreaterThanOrEqual, startDate),
                new ScanCondition("CreatedDate", ScanOperator.LessThan, endDate)
            };

            return await _context.ScanAsync<Product>(conditions).GetRemainingAsync();
        }

        public async Task<Product?> UpdateProductAsync(string id, UpdateProductRequest request)
        {
            var product = await _context.LoadAsync<Product>(id);
            if (product == null) return null;

            product.Name = request.Name;
            product.Brand = request.Brand;
            product.Unit = request.Unit;
            product.Quantity = request.Quantity;
            product.UpdatedDate = DateTime.UtcNow;

            await _context.SaveAsync(product);
            return product;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var product = await _context.LoadAsync<Product>(id);
            if (product == null) return false;

            await _context.DeleteAsync(product);
            return true;
        }
    }
}
