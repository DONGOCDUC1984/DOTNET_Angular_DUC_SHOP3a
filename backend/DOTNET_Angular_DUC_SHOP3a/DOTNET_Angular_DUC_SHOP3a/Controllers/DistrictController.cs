﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_Angular_DUC_SHOP3a.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DistrictController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.districtRepository.GetAllWithDetails();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.districtRepository.GetById(id);
            return Ok(data);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddUpdate(DistrictAddUpdateDTO modelDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _unitOfWork.districtRepository.AddUpdate(modelDTO);
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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.districtRepository.Delete(id);
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
    }
}
