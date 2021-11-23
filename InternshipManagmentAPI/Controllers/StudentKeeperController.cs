using Administration.Interface;
using DataAcess.Data;
using InternshipManagmentAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace InternshipManagmentAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentKeeperController : Controller
    {
        private readonly StudentKeeperInterface _studentKeeperInterface;

        public StudentKeeperController(StudentKeeperInterface studentKeeperInterface)
        {
            _studentKeeperInterface = studentKeeperInterface;
        }

        [Authorize(Roles = "Student,Company")]
        [HttpGet("keepers/{studentId}")]
        public async Task<IActionResult> GetKeepers(int? studentId)
        {
            if (studentId == null)
            {
                return BadRequest();
            }

            var keeperData = await _studentKeeperInterface.GetAllKeeperIdByStudentId(studentId.Value);
            if (keeperData == null)
            {
                return NotFound();
            }
            return Ok(keeperData);
        }

        [Authorize(Roles = "Keeper")]
        [HttpGet("students/{keeperId}")]
        public async Task<IActionResult> GetStudents(int? keeperId)
        {
            if (keeperId == null)
            {
                return BadRequest();
            }

            var keeperData = await _studentKeeperInterface.GetAllStudentIdByKeeperId(keeperId.Value);
            if (keeperData == null)
            {
                return NotFound();
            }
            return Ok(keeperData);
        }

        [Authorize(Roles = "Keeper")]
        [HttpDelete("delete/{StudentId}")]
        public async Task<IActionResult> DeleteStudent(int? StudentId)
        {
            if (StudentId == null)
            {
                return BadRequest();
            }

            var diaryData = await _studentKeeperInterface.DeleteStudentByStudentId(StudentId.Value);
            return Ok(diaryData);
        }

        [Authorize(Roles = "Student")]
        [HttpDelete("delete/{KeeperId}")]
        public async Task<IActionResult> DeleteKeeper(int? KeeperId)
        {
            if (KeeperId == null)
            {
                return BadRequest();
            }

            var diaryData = await _studentKeeperInterface.DeleteKeeperByKeeperId(KeeperId.Value);
            return Ok(diaryData);
        }

        [Authorize(Roles = "Keeper")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateKeeperStudent(StudentKeeperDTO StudentKeeperDTO)
        {
            if (StudentKeeperDTO == null)
            {
                return BadRequest();
            }

            var StudentKeeperData = await _studentKeeperInterface.CreateStudentKeeper(StudentKeeperDTO);
            if (StudentKeeperData == null)
            {
                return NotFound();
            }
            return Ok(StudentKeeperData);
        }
    }
}

