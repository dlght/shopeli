using Microsoft.AspNetCore.Mvc;
using Shopeli.ShoppingCart.API.Models;
using Shopeli.ShoppingCart.API.Services;

namespace Shopeli.ShoppingCart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListsController : ControllerBase
    {
        private readonly IShoppingListService _shoppingListService;

        public ShoppingListsController(IShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingList>> CreateShoppingList(CreateShoppingListRequest request)
        {
            try
            {
                var shoppingList = await _shoppingListService.CreateShoppingListAsync(request);
                return CreatedAtAction(nameof(GetShoppingList), new { id = shoppingList.Id }, shoppingList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating shopping list: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingList>> GetShoppingList(string id)
        {
            var shoppingList = await _shoppingListService.GetShoppingListAsync(id);
            return shoppingList == null ? NotFound() : Ok(shoppingList);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingList>>> GetShoppingLists()
        {
            try
            {
                var shoppingLists = await _shoppingListService.GetShoppingListsAsync();
                return Ok(shoppingLists);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving shopping lists: {ex.Message}");
            }
        }

        [HttpGet("by-period")]
        public async Task<ActionResult<IEnumerable<ShoppingList>>> GetShoppingListsByPeriod(
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var shoppingLists = await _shoppingListService.GetShoppingListsByPeriodAsync(startDate, endDate);
                return Ok(shoppingLists);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving shopping lists by period: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ShoppingList>> UpdateShoppingList(string id, UpdateShoppingListRequest request)
        {
            try
            {
                var shoppingList = await _shoppingListService.UpdateShoppingListAsync(id, request);
                return shoppingList == null ? NotFound() : Ok(shoppingList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating shopping list: {ex.Message}");
            }
        }

        [HttpPost("{id}/items")]
        public async Task<ActionResult<ShoppingList>> AddItem(string id, AddItemRequest request)
        {
            try
            {
                var shoppingList = await _shoppingListService.AddItemToShoppingListAsync(id, request);
                return shoppingList == null ? NotFound() : Ok(shoppingList);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding item: {ex.Message}");
            }
        }

        [HttpPut("{id}/items/{productId}/quantity")]
        public async Task<ActionResult<ShoppingList>> UpdateItemQuantity(string id, string productId, [FromBody] UpdateQuantityRequest request)
        {
            try
            {
                var shoppingList = await _shoppingListService.UpdateItemQuantityAsync(id, productId, request.Quantity);
                return shoppingList == null ? NotFound() : Ok(shoppingList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating quantity: {ex.Message}");
            }
        }

        [HttpDelete("{id}/items/{productId}")]
        public async Task<ActionResult<ShoppingList>> RemoveItem(string id, string productId)
        {
            try
            {
                var shoppingList = await _shoppingListService.RemoveItemFromShoppingListAsync(id, productId);
                return shoppingList == null ? NotFound() : Ok(shoppingList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error removing item: {ex.Message}");
            }
        }

        [HttpPatch("{id}/items/{productId}/toggle")]
        public async Task<ActionResult<ShoppingList>> ToggleItemCompletion(string id, string productId)
        {
            try
            {
                var shoppingList = await _shoppingListService.ToggleItemCompletionAsync(id, productId);
                return shoppingList == null ? NotFound() : Ok(shoppingList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error toggling item completion: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShoppingList(string id)
        {
            try
            {
                var success = await _shoppingListService.DeleteShoppingListAsync(id);
                return success ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting shopping list: {ex.Message}");
            }
        }
    }
}
