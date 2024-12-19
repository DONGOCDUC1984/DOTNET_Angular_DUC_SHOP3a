using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_Angular_DUC_SHOP3a.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.categoryRepository.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.categoryRepository.GetById(id);
            return Ok(data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUpdate(Category model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Validatation failed";
            }
            var result = await _unitOfWork.categoryRepository.AddUpdate(model);
            await _unitOfWork.Save();
            status.StatusCode = result ? 1 : 0;
            status.Message = result ? "Saved successfully" : "Error has occured";
            return Ok(status);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.categoryRepository.Delete(id);
            await _unitOfWork.Save();
            var status = new Status
            {
                StatusCode = result ? 1 : 0,
                Message = result ? "deleted successfully" : "Error has occured"
            };
            return Ok(status);
        }
    }
}
