using System.ComponentModel.DataAnnotations;

namespace CartAPI.Model
{
    public class Product
    {
        [Key]
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string ProductDescription { get; set; }
        public Category ProductCategory { get; set; }
        public string ProductImageUrl { get; set; }
    }
}