using Shopeli.ShoppingCart.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopeli.ShoppingCart.API.Services
{
    public interface IShoppingListService
    {
        Task<ShoppingList> CreateShoppingListAsync(CreateShoppingListRequest request);
        Task<ShoppingList?> GetShoppingListAsync(string id);
        Task<IEnumerable<ShoppingList>> GetShoppingListsAsync();
        Task<IEnumerable<ShoppingList>> GetShoppingListsByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<ShoppingList?> UpdateShoppingListAsync(string id, UpdateShoppingListRequest request);
        Task<ShoppingList?> AddItemToShoppingListAsync(string listId, AddItemRequest request);
        Task<ShoppingList?> UpdateItemQuantityAsync(string listId, string productId, decimal quantity);
        Task<ShoppingList?> RemoveItemFromShoppingListAsync(string listId, string productId);
        Task<ShoppingList?> ToggleItemCompletionAsync(string listId, string productId);
        Task<bool> DeleteShoppingListAsync(string id);
    }
}
