using System.ComponentModel.DataAnnotations;

namespace CartAPI.Model
{
    public class CartHeader
    {
        [Key]
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
    }
}
