using Amazon.DynamoDBv2.DataModel;
using System.ComponentModel.DataAnnotations;

namespace Shopeli.ProductManagement.API.Models
{
    [DynamoDBTable("Products")]
    public class Product
    {
        [DynamoDBHashKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [DynamoDBProperty]
        public string Name { get; set; } = string.Empty;

        [DynamoDBProperty]
        public string Brand { get; set; } = string.Empty;

        [DynamoDBProperty]
        public MeasurementUnit Unit { get; set; }

        [DynamoDBProperty]
        public decimal Quantity { get; set; }

        [DynamoDBProperty]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [DynamoDBProperty]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }

    public enum MeasurementUnit
    {
        Gram = 0,
        Kilogram = 1,
        Liter = 2,
        Milliliter = 3,
        Piece = 4
    }

    public class CreateProductRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Brand { get; set; } = string.Empty;

        public MeasurementUnit Unit { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Quantity { get; set; }
    }

    public class UpdateProductRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Brand { get; set; } = string.Empty;

        public MeasurementUnit Unit { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Quantity { get; set; }
    }
}
