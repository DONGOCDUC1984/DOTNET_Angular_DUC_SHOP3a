using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_Angular_DUC_SHOP3a.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetCartByUserId(string UserId)
        {
            var cart = await _unitOfWork.cartRepository.GetCartByUserId(UserId);
            var length = await _unitOfWork.cartRepository.GetCartLen(UserId);
            var totalCost = await _unitOfWork.cartRepository.GetTotalCost(UserId);
            return Ok(new { cart = cart, length = length, totalCost = totalCost });
        }

        [HttpGet("AddCartItem")]
        public async Task<IActionResult> AddCartItem(string UserId, int ProductId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _unitOfWork.cartRepository.AddCartItem(UserId, ProductId);
            var cart = await _unitOfWork.cartRepository.GetCartByUserId(UserId);
            var length = await _unitOfWork.cartRepository.GetCartLen(UserId);
            var totalCost = await _unitOfWork.cartRepository.GetTotalCost(UserId);
            return Ok(new { cart = cart, length = length, totalCost = totalCost });
        }
        [HttpGet("DecreaseCartItem")]
        public async Task<IActionResult> DecreaseCartItem(string UserId, int ProductId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _unitOfWork.cartRepository.DecreaseCartItem(UserId, ProductId);
            var cart = await _unitOfWork.cartRepository.GetCartByUserId(UserId);
            var length = await _unitOfWork.cartRepository.GetCartLen(UserId);
            var totalCost = await _unitOfWork.cartRepository.GetTotalCost(UserId);
            return Ok(new { cart = cart, length = length, totalCost = totalCost });
        }
        [HttpDelete("RemoveCartItem")]
        public async Task<IActionResult> RemoveCartItem(string UserId, int ProductId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _unitOfWork.cartRepository.RemoveCartItem(UserId, ProductId);
            var cart = await _unitOfWork.cartRepository.GetCartByUserId(UserId);
            var length = await _unitOfWork.cartRepository.GetCartLen(UserId);
            var totalCost = await _unitOfWork.cartRepository.GetTotalCost(UserId);
            return Ok(new { cart = cart, length = length, totalCost = totalCost });
        }
    }
}
