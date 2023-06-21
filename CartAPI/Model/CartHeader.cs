using System.ComponentModel.DataAnnotations;

namespace CartAPI.Model
{
    public class CartHeader
    {
        [Key]
        public Guid CartHeaderId { get; set; }
        public string UserId { get; set; }
    }
}
