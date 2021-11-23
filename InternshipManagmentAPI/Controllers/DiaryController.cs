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
    public class DiaryController : Controller
    {
        private readonly DiaryInterface _diaryInterface;

        public DiaryController(DiaryInterface diaryInterface)
        {
            _diaryInterface = diaryInterface;
        }

        [Authorize(Roles = "Keeper,Student")]
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetDiaries(int? studentId)
        {
            if (studentId == null)
            {
                return BadRequest();
            }

            var diaryData = await _diaryInterface.GetAllDiariesByStudent(studentId.Value);
            if (diaryData == null)
            {
                return NotFound();
            }
            return Ok(diaryData);
        }

        [Authorize(Roles = "Student")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateDiary(DiaryDTO diaryDTO)
        {
            if (diaryDTO == null)
            {
                return BadRequest();
            }

            var diaryData = await _diaryInterface.CreateDiary(diaryDTO);
            if (diaryData == null)
            {
                return NotFound();
            }
            return Ok(diaryData);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("{diaryId}")]
        public async Task<IActionResult> GetDiary(int? diaryId)
        {
            if (diaryId == null)
            {
                return BadRequest();
            }

            var diaryData = await _diaryInterface.GetDiary(diaryId.Value);
            if (diaryData == null)
            {
                return NotFound();
            }
            return Ok(diaryData);
        }

        [Authorize(Roles = "Student")]
        [HttpPut("update/{diaryId}")]
        public async Task<IActionResult> UpdateDiary(int? diaryId,[FromBody] DiaryDTO diaryDTO)
        {
            if (diaryId == null)
            {
                return BadRequest();
            }
            diaryDTO = await _diaryInterface.UpdateDiary(diaryId.Value, diaryDTO);

            if (diaryDTO == null)
            {
                return NotFound();
            }

            return Ok(diaryDTO);
        }

        [Authorize(Roles = "Student")]
        [HttpDelete("delete/{diaryId}")]
        public async Task<IActionResult> DeleteDiary(int? diaryId)
        {
            if (diaryId == null)
            {
                return BadRequest();
            }

            var diaryData = await _diaryInterface.DeleteDiary(diaryId.Value);
            return Ok(diaryData);
        }
    }
}
