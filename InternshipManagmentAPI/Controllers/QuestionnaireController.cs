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
    public class QuestionnaireController : Controller
    {
        private readonly QuestionnaireInterface _questionnaireInterface;
        public QuestionnaireController(QuestionnaireInterface questionnaireInterface)
        {
            _questionnaireInterface = questionnaireInterface;
        }

        [Authorize(Roles = "Keeper,Student")]
        [HttpGet("student/{StudentId}")]
        public async Task<IActionResult> GetQuestionnaireByStudent(int? StudentId)
        {
            var questionnaireData = await _questionnaireInterface.GetQuestionnaireByStudentId(StudentId.Value);
            return Ok(questionnaireData);
        }

        [Authorize(Roles = "Keeper,Student")]
        [HttpGet("student&keeper/{StudentId}/{KeeperId}")]
        public async Task<IActionResult> GetQuestionnaireByStudentAndKeeperId(int? StudentId, int? KeeperId)
        {
            var questionnaireData = await _questionnaireInterface.GetQuestionnaireByKeeperAndStudent(StudentId.Value, KeeperId.Value);
            return Ok(questionnaireData);
        }

        [Authorize(Roles = "Keeper")]
        [HttpDelete("delete/{QuestionnaireId}")]
        public async Task<IActionResult> DeleteQuestionnaire(int? QuestionnaireId)
        {
            if (QuestionnaireId == null)
            {
                return BadRequest();
            }

            var questionnaireData = await _questionnaireInterface.DeleteQuestionnaire(QuestionnaireId.Value);
            return Ok(questionnaireData);
        }

        [Authorize(Roles = "Keeper")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateQuestionnaire(QuestionnaireModelDTO questionnaireModelDTO)
        {
            if (questionnaireModelDTO == null)
            {
                return BadRequest();
            }

            var questionnaireData = await _questionnaireInterface.CreateQuestionnaire(questionnaireModelDTO);
            if (questionnaireData == null)
            {
                return NotFound();
            }
            return Ok(questionnaireData);
        }

        [Authorize(Roles = "Keeper")]
        [HttpPut("update/{QuestionnaireId}")]
        public async Task<IActionResult> UpdateQuestionnaire(int? QuestionnaireId, [FromBody] QuestionnaireModelDTO questionnaireModelDTO)
        {
            if (QuestionnaireId == null)
            {
                return BadRequest();
            }
            questionnaireModelDTO = await _questionnaireInterface.UpdateQuestionnaire(QuestionnaireId.Value, questionnaireModelDTO);

            if (questionnaireModelDTO == null)
            {
                return NotFound();
            }

            return Ok(questionnaireModelDTO);
        }
    }
}

