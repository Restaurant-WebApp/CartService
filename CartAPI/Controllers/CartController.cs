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

        /*[HttpPost]
        public async Task<object> ClearCart(string userId)
        {
            try
            {
                Boolean bol = await _cartRepository.ClearCart(userId);
                _response.IsSuccess = bol;

            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);

            }
            return _response;
        }*/

        [HttpPost]
        [Route("/AddCart")]
        public async Task<object> AddCart(Cart cart)
        {
            try
            {
                Cart cartDto = await _cartRepository.CreateUpdateCart(cart);
                _response.Result = cartDto;

            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Route("/UpdateCart")]
        public async Task<object> UpdateCart(Cart cart)
        {
            try
            {
                Cart cartDto = await _cartRepository.CreateUpdateCart(cart);
                _response.Result = cartDto;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("/GetCart/{userId}")]
        public async Task<object> GetCartByUserId(string userId)
        {
            try
            {
                Cart cartDto = await _cartRepository.GetCartByUserId(userId);
                _response.Result = cartDto;

            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Route("/RemoveCart")]
        public async Task<object> RemoveFromCart([FromBody] int cartDetailsId)
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
