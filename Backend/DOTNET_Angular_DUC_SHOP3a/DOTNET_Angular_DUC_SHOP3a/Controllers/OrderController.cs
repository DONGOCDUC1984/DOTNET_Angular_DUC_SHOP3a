using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_Angular_DUC_SHOP3a.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpGet("Add")]
        public async Task<IActionResult> Add(string UserId,
            string UserTel, string UserAddress, double totalCost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _unitOfWork.orderRepository.Add(UserId, UserTel, UserAddress, totalCost);
            if (result)
            {
                var data = await GetAll();
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }

        }
        // [Authorize(Roles = "Admin")]
        // The following line is equivalent to the above line
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _unitOfWork.orderRepository.Delete(id);
            await _unitOfWork.Save();
            if (result)
            {
                var data = await GetAll();
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.orderRepository.GetAllWithDetails();
            return Ok(data);
        }
        [HttpGet("{UserId}")]
        [Authorize]
        public async Task<IActionResult> GetOrdersByUserId(string UserId)
        {
            var data = await _unitOfWork.orderRepository.GetOrdersByUserId(UserId);
            return Ok(data);
        }
    }
}
