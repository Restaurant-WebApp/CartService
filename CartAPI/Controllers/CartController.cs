using CartAPI.Messages;
using CartAPI.Model;
using CartAPI.RabbitMqSender;
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
        private readonly IRabbitMqCartSender _rabbitMqSender;
        public CartController(ICartRepository cartRepository, IRabbitMqCartSender rabbitMqCartSender)
        {
            _cartRepository = cartRepository;
            this._response = new Response();
            _rabbitMqSender = rabbitMqCartSender;
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

        [HttpPost]
        [Route("/Checkout")]
        public async Task<object> Checkout(CheckoutHeader checkoutHeader)
        {
            try
            {
                /*Cart cart = await _cartRepository.GetCartByUserId(checkoutHeader.UserId);
                if (cart != null)
                {
                    return BadRequest();
                }
                checkoutHeader.CartDetails = cart.CartDetails;*/

                if(checkoutHeader.Email == null)
                {
                    _response.IsSuccess=false;
                    _response.ErrorMessages = new List<string>() { "The email adress cannot be null" };
                }

                // Publish a queue
                _rabbitMqSender.SendMessage(checkoutHeader, "checkoutqueue");
                await _cartRepository.ClearCart(checkoutHeader.UserId);

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
