using Microsoft.EntityFrameworkCore;

namespace CartAPI.Model
{
    [Keyless]
    public class Cart
    {
        public CartHeader CartHeader { get; set; }
        public IEnumerable<CartDetails> CartDetails { get; set; }
    }
}
