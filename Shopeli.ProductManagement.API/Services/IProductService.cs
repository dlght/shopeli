using Shopeli.ProductManagement.API.Models;

namespace Shopeli.ProductManagement.API.Services
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(CreateProductRequest request);
        Task<Product?> GetProductAsync(string id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsByDateAsync(DateTime date);
        Task<Product?> UpdateProductAsync(string id, UpdateProductRequest request);
        Task<bool> DeleteProductAsync(string id);
    }
}
