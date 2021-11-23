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
    public class StudentAvatarController : Controller
    {
        private readonly StudentAvatarsInterface _studentAvatarsInterface;
        public StudentAvatarController(StudentAvatarsInterface studentAvatarsInterface)
        {
            _studentAvatarsInterface = studentAvatarsInterface;
        }


        [Authorize(Roles = "Student")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateStudentAvatar(StudentAvatarsDTO studentAvatarsDTO)
        {
            if (studentAvatarsDTO == null)
            {
                return BadRequest();
            }

            var studentAvatarsData = await _studentAvatarsInterface.CreateAvatar(studentAvatarsDTO);
            if (studentAvatarsData == null)
            {
                return NotFound();
            }
            return Ok(studentAvatarsData);
        }

        [Authorize(Roles = "Student")]
        [HttpDelete("delete/{studentId}")]
        public async Task<IActionResult> DeleteStudentAvatar(int? studentId)
        {
            if (studentId == null)
            {
                return BadRequest();
            }

            var diaryData = await _studentAvatarsInterface.DeleteAvatarByStudentId(studentId.Value);
            return Ok(diaryData);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("avatar/{studentId}")]
        public async Task<IActionResult> GetStudentAvatar(int? studentId)
        {
            var diaryData = await _studentAvatarsInterface.GetStudentAvatar(studentId.Value);
            return Ok(diaryData);
        }
    }
}
