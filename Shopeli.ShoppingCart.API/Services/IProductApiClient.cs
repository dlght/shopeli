using Shopeli.ShoppingCart.API.Models;

namespace Shopeli.ShoppingCart.API.Services
{
    public interface IProductApiClient
    {
        Task<ProductDto?> GetProductAsync(string id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }
}
