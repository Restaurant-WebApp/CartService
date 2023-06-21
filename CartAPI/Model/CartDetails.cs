using System.ComponentModel.DataAnnotations.Schema;

namespace CartAPI.Model
{
    public class CartDetails
    {
        public Guid CartDetailsId { get; set; }
        public Guid CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int Count { get; set; }
    }
}
