using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shopeli.ShoppingCart.API.Models
{
    [DynamoDBTable("ShoppingLists")]
    public class ShoppingList
    {
        [DynamoDBHashKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [DynamoDBProperty]
        public string Name { get; set; } = string.Empty;

        [DynamoDBProperty]
        public DateTime StartDate { get; set; }

        [DynamoDBProperty]
        public DateTime EndDate { get; set; }

        [DynamoDBProperty]
        public List<ShoppingListItem> Items { get; set; } = new();

        [DynamoDBProperty]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [DynamoDBProperty]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }

    public class ShoppingListItem
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public MeasurementUnit Unit { get; set; }
        public decimal Quantity { get; set; }
        public bool IsCompleted { get; set; } = false;
    }

    public enum MeasurementUnit
    {
        Gram = 0,
        Kilogram = 1,
        Liter = 2,
        Milliliter = 3,
        Piece = 4
    }

    public class CreateShoppingListRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class UpdateShoppingListRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class AddItemRequest
    {
        [Required]
        public string ProductId { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal Quantity { get; set; }
    }

    public class UpdateQuantityRequest
    {
        [Range(0.01, double.MaxValue)]
        public decimal Quantity { get; set; }
    }

    public class ProductDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public MeasurementUnit Unit { get; set; }
        public decimal Quantity { get; set; }
    }
}
