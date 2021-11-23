using Administration.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyImageController : Controller
    {
        private readonly CompanyImageInterface _companImageInterface;
        public CompanyImageController(CompanyImageInterface companImageInterface)
        {
            _companImageInterface = companImageInterface;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreateCompanyImage(CompanyImageDTO companyImageDTO)
        {
            if (companyImageDTO == null)
            {
                return BadRequest();
            }

            var ImageData = await _companImageInterface.CreateImage(companyImageDTO);
            if (ImageData == null)
            {
                return NotFound();
            }
            return Ok(ImageData);
        }

        [AllowAnonymous]
        [HttpDelete("delete/{companyId}")]
        public async Task<IActionResult> DeleteImage(int? companyId)
        {
            if (companyId == null)
            {
                return BadRequest();
            }

            var data = await _companImageInterface.DeleteImageByCompanyId(companyId.Value);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("image/{companyId}")]
        public async Task<IActionResult> GetCompanyImage(int? companyId)
        {
            var imageData = await _companImageInterface.GetCompanyImage(companyId.Value);
            return Ok(imageData);
        }

        [AllowAnonymous]
        [HttpGet("images/all")]
        public async Task<IActionResult> GetAllCompanyImages()
        {
            var imageData = await _companImageInterface.GetAllImages();
            if (imageData == null)
            {
                return NotFound();
            }
            return Ok(imageData);
        }
    }
}
