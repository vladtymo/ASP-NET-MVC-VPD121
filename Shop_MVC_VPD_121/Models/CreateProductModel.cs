using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Shop_MVC_VPD_121.Models
{
    // DTO - data transfer object
    public class CreateProductModel
    {
        [MinLength(3)]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be greather than 0.")]
        public decimal Price { get; set; }

        public IFormFile? Image { get; set; }
        
        public int CategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Discount must be greather than 0.")]
        public int? Discount { get; set; }

        public bool InStock { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public string? Description { get; set; }
    }
}
