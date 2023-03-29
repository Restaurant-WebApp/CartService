using CartAPI.Model;

namespace CartAPI.Repository
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserId(string userId);
        Task<Cart> CreateUpdateCart(Cart cart);
        Task<bool> RemoveFromCart(int cartDetailsId);
        Task<bool> ClearCart(string userId);

    }
}
