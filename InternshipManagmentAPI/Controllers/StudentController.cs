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
    public class StudentController : Controller
    {
        private readonly StudentInterface _studentInterface;
        private readonly UserManager<User> _user;
        private readonly SignInManager<User> _signIn;
        private readonly Security _security;

        public StudentController(UserManager<User> user,  SignInManager<User> signIn,
            StudentInterface studentInterface, IOptions<Security> options)
        {
            _user = user;
            _signIn = signIn;
            _studentInterface = studentInterface;
            _security = options.Value;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SingUp([FromBody] StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                var student = new User()
                {
                    UserName = studentDTO.email,
                    Email = studentDTO.email,
                    Name = studentDTO.lastName,
                    EmailConfirmed = true
                };

                var StudentData = studentDTO;

                var result = await _user.CreateAsync(student, studentDTO.Password);
                var roleResult = await _user.AddToRoleAsync(student, AuthenticationData.StudentRole);

                if (!result.Succeeded || !roleResult.Succeeded || !roleResult.Succeeded && !result.Succeeded)
                {
                    return BadRequest();
                }
                await _studentInterface.CreateStudent(StudentData);
                return StatusCode(201);
            }
            else
            {
                return BadRequest();
            }
        
        }

        [AllowAnonymous]
        [HttpPost("singin")]
        public async Task<IActionResult> SingIn([FromBody] AuthenticationUser authentication)
        {
            var result = await _signIn.PasswordSignInAsync(authentication.Email,
                authentication.Password, false, false);

            if (result.Succeeded)
            {
                var studentDTO = await _studentInterface.GetStudentByEmail(authentication.Email);
                if (studentDTO == null)
                {
                    return Unauthorized();
                }

                var credentials = GetSigninCredentials();
                var claims = await GetClaims(studentDTO);

                var tokenJwt = new JwtSecurityToken(
                    issuer: _security.Issuer,
                    audience: _security.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: credentials);

                var newToken = new JwtSecurityTokenHandler().WriteToken(tokenJwt);
                return base.Ok(new SignInAuthentication
                {
                    isAuthenticationSuccessful = true,
                    Token = newToken,
                    studentDTO = new StudentDTO
                    {
                        lastName = studentDTO.lastName,
                        StudentId = studentDTO.StudentId,
                        email = studentDTO.email
                    }
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize(Roles = "Student,Keeper")]
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudent(int? studentId)
        {
            if (studentId == null)
            {
                return BadRequest();
            }

            var studentData = await _studentInterface.GetStudent(studentId.Value);
            if (studentData == null)
            {
                return NotFound();
            }
            return Ok(studentData);
        }

        [Authorize(Roles = "Keeper")]
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetStudentByEmail(string email)
        {
            if (email == null)
            {
                return BadRequest();
            }

            var studentData = await _studentInterface.GetStudentByEmail(email);
            if (studentData == null)
            {
                return NotFound();
            }
            return Ok(studentData);
        }

        [AllowAnonymous]
        [HttpGet("index/{index}")]
        public async Task<IActionResult> CheckIndex(string index)
        {
            var studentData = await _studentInterface.IsIndexUnique(index);
            return Ok(studentData);
        }


        [Authorize(Roles = "Student")]
        [HttpPut("update/{studentId}")]
        public async Task<ActionResult<StudentDTO>> UpdateStudent(int? studentId,StudentDTO studentDTO)
        {
            if (studentId == null)
            {
                return BadRequest();
            }
            studentDTO = await _studentInterface.UpdateStudent(studentId.Value, studentDTO);

            if (studentDTO == null)
            {
                return NotFound();
            }

            var user = await _user.FindByEmailAsync(studentDTO.email);
            if(user == null)
            {
                return NotFound();
            }
            var tokenPassword = await _user.GeneratePasswordResetTokenAsync(user);
            var resultPassword = await _user.ResetPasswordAsync(user, tokenPassword, studentDTO.Password);

            if (resultPassword == null)
            {
                return BadRequest();
            }

            return Ok(studentDTO);
        }
        private SigningCredentials GetSigninCredentials()
        {
            var token = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_security.Key));
            return new SigningCredentials(token, SecurityAlgorithms.HmacSha256);
        }
            
        private async Task<List<Claim>> GetClaims(StudentDTO studentDTO)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,studentDTO.email),
                new Claim(ClaimTypes.Email,studentDTO.email),
                new Claim("StudentId", studentDTO.StudentId.ToString())
            };

            var roles = await _user.GetRolesAsync(await _user.FindByEmailAsync(studentDTO.email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }


    }
}

