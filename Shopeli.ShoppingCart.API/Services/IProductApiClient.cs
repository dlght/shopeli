using Shopeli.ShoppingCart.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopeli.ShoppingCart.API.Services
{
    public interface IProductApiClient
    {
        Task<ProductDto?> GetProductAsync(string id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }
}
