using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Shopeli.ShoppingCart.API.Models;

namespace Shopeli.ShoppingCart.API.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly IDynamoDBContext _context;
        private readonly IProductApiClient _productApiClient;

        public ShoppingListService(IDynamoDBContext context, IProductApiClient productApiClient)
        {
            _context = context;
            _productApiClient = productApiClient;
        }

        public async Task<ShoppingList> CreateShoppingListAsync(CreateShoppingListRequest request)
        {
            var shoppingList = new ShoppingList
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            await _context.SaveAsync(shoppingList);
            return shoppingList;
        }

        public async Task<ShoppingList?> GetShoppingListAsync(string id)
        {
            return await _context.LoadAsync<ShoppingList>(id);
        }

        public async Task<IEnumerable<ShoppingList>> GetShoppingListsAsync()
        {
            var conditions = new List<ScanCondition>();
            return await _context.ScanAsync<ShoppingList>(conditions).GetRemainingAsync();
        }

        public async Task<IEnumerable<ShoppingList>> GetShoppingListsByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("StartDate", ScanOperator.LessThanOrEqual, endDate),
                new ScanCondition("EndDate", ScanOperator.GreaterThanOrEqual, startDate)
            };

            return await _context.ScanAsync<ShoppingList>(conditions).GetRemainingAsync();
        }

        public async Task<ShoppingList?> UpdateShoppingListAsync(string id, UpdateShoppingListRequest request)
        {
            var shoppingList = await _context.LoadAsync<ShoppingList>(id);
            if (shoppingList == null) return null;

            shoppingList.Name = request.Name;
            shoppingList.StartDate = request.StartDate;
            shoppingList.EndDate = request.EndDate;
            shoppingList.UpdatedDate = DateTime.UtcNow;

            await _context.SaveAsync(shoppingList);
            return shoppingList;
        }

        public async Task<ShoppingList?> AddItemToShoppingListAsync(string listId, AddItemRequest request)
        {
            var shoppingList = await _context.LoadAsync<ShoppingList>(listId);
            if (shoppingList == null) return null;

            // Get product details from Product Service
            var product = await _productApiClient.GetProductAsync(request.ProductId);
            if (product == null) throw new ArgumentException("Product not found");

            var existingItem = shoppingList.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (existingItem != null)
            {
                // Accumulate quantity instead of replacing
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                shoppingList.Items.Add(new ShoppingListItem
                {
                    ProductId = request.ProductId,
                    ProductName = product.Name,
                    Brand = product.Brand,
                    Unit = product.Unit,
                    Quantity = request.Quantity
                });
            }

            shoppingList.UpdatedDate = DateTime.UtcNow;
            await _context.SaveAsync(shoppingList);
            return shoppingList;
        }

        public async Task<ShoppingList?> UpdateItemQuantityAsync(string listId, string productId, decimal quantity)
        {
            var shoppingList = await _context.LoadAsync<ShoppingList>(listId);
            if (shoppingList == null) return null;

            var item = shoppingList.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                shoppingList.UpdatedDate = DateTime.UtcNow;
                await _context.SaveAsync(shoppingList);
            }

            return shoppingList;
        }

        public async Task<ShoppingList?> RemoveItemFromShoppingListAsync(string listId, string productId)
        {
            var shoppingList = await _context.LoadAsync<ShoppingList>(listId);
            if (shoppingList == null) return null;

            shoppingList.Items.RemoveAll(i => i.ProductId == productId);
            shoppingList.UpdatedDate = DateTime.UtcNow;

            await _context.SaveAsync(shoppingList);
            return shoppingList;
        }

        public async Task<ShoppingList?> ToggleItemCompletionAsync(string listId, string productId)
        {
            var shoppingList = await _context.LoadAsync<ShoppingList>(listId);
            if (shoppingList == null) return null;

            var item = shoppingList.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.IsCompleted = !item.IsCompleted;
                shoppingList.UpdatedDate = DateTime.UtcNow;
                await _context.SaveAsync(shoppingList);
            }

            return shoppingList;
        }

        public async Task<bool> DeleteShoppingListAsync(string id)
        {
            var shoppingList = await _context.LoadAsync<ShoppingList>(id);
            if (shoppingList == null) return false;

            await _context.DeleteAsync(shoppingList);
            return true;
        }
    }
}
