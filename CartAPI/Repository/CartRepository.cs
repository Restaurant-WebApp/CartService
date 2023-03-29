using CartAPI.Data;
using CartAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly CartAppDbContext _context;

        public Task<bool> ClearCart(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> CreateUpdateCart(Cart cart)
        {
            //check if product exits in database, if not creat it!
            var prodInDb = await _context.Products.FirstOrDefaultAsync(u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId);
            
            if (prodInDb == null)
            {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            //check if header is null
            var header = await _context.CartHeaders.FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);

            if (header == null)
            {
                //create header and details
                _context.CartHeaders.Add(header);
                await _context.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            //if header is not null
            //check if details has same product
            //if it has then update the count
            //else create details
        }

        public Task<Cart> GetCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromCart(int cartDetailsId)
        {
            throw new NotImplementedException();
        }
    }
}
