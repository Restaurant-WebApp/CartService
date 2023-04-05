using CartAPI.Model;
using CartAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private Response _response;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            this._response = new Response();
        }

        [HttpPost]
        public async Task<bool> ClearCart(string userId)
        {
            return await _cartRepository.ClearCart(userId);

        }

        [HttpGet]
        [Route("/UpdateCart")]
        public async Task<object> CreateUpdateCart(Cart cart)
        {
            try
            {
                Cart cartDto = await _cartRepository.CreateUpdateCart(cart);
                _response.Result = cartDto;

            }catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString()};
            }
            return _response;
        }

        [HttpGet]
        [HttpPost("/GetCart/{userId}")]
        public async Task<object> GetCartByUserId(string userId)
        {
            try
            {
                Cart cartDto = await _cartRepository.GetCartByUserId(userId);
                _response.Result = cartDto;

            }catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [HttpPost("RemoveCart")]
        public async Task<object> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                Boolean bol = await _cartRepository.RemoveFromCart(cartDetailsId);
                _response.IsSuccess = bol;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
