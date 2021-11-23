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
        public class InternshipController : Controller
        {
        private readonly InternshipInterface _internshipInterface;
        public InternshipController(InternshipInterface internshipInterface)
            {
            _internshipInterface = internshipInterface;
        }

        [AllowAnonymous]
        [HttpGet("{InternshipId}")]
        public async Task<IActionResult> GetInternship(int? InternshipId)
        {
            if (InternshipId == null)
            {
                return BadRequest();
            }

            var internshipData = await _internshipInterface.GetInternship(InternshipId.Value);
            if (internshipData == null)
            {
                return NotFound();
            }
            return Ok(internshipData);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var internships = await _internshipInterface.GetAllInternships();
            if (internships == null)
            {
                return NotFound();
            }
            return Ok(internships);
        }

        [AllowAnonymous]
        [HttpGet("company/{CompanyId}")]
        public async Task<IActionResult> GetInternshipByCompanyId(int? CompanyId)
        {
            var internships = await _internshipInterface.GetAllCompanyInternships(CompanyId.Value);
            return Ok(internships);
        }


        [Authorize(Roles = "Company")]
        [HttpDelete("delete/{internshipId}")]
        public async Task<IActionResult> DeleteInternship(int? internshipId)
        {
            if (internshipId == null)
            {
                return BadRequest();
            }

            var diaryData = await _internshipInterface.DeleteInternship(internshipId.Value);
            return Ok(diaryData);
        }


        [Authorize(Roles = "Company")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateInternship(InternshipDTO internshipDTO)
        {
            if (internshipDTO == null)
            {
                return BadRequest();
            }

            var internshipData = await _internshipInterface.CreateInternship(internshipDTO);
            if (internshipData == null)
            {
                return NotFound();
            }
            return Ok(internshipData);
        }

        [Authorize(Roles = "Company")]
        [HttpPut("update/{internshipId}")]
        public async Task<IActionResult> UpdateInternship(int? internshipId, [FromBody] InternshipDTO internshipDTO)
        {
            if (internshipId == null)
            {
                return BadRequest();
            }
            internshipDTO = await _internshipInterface.UpdateInternship(internshipId.Value, internshipDTO);

            if (internshipDTO == null)
            {
                return NotFound();
            }

            return Ok(internshipDTO);
        }
    }

}
