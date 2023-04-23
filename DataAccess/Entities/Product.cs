using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [MinLength(3)]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be greather than 0.")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Discount must be greather than 0.")]
        public int? Discount { get; set; }

        public bool InStock { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public string? Description { get; set; }

        // ---------- navigation properties
        public Category? Category { get; set; }
    }
}
