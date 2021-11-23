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
    public class CompanyController : Controller
    {
        private readonly CompanyInterface _companyInterface;
        private readonly UserManager<User> _user;
        private readonly SignInManager<User> _signIn;
        private readonly Security _security;

        public CompanyController(UserManager<User> user,  SignInManager<User> signIn,
            CompanyInterface companyInterface, IOptions<Security> options)
        {
            _user = user;
            _signIn = signIn;
            _companyInterface = companyInterface;
            _security = options.Value;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SingUp([FromBody] CompanyDTO companyDTO)
        {
            if (ModelState.IsValid)
            {
                var company = new User()
                {
                    UserName = companyDTO.email,
                    Email = companyDTO.email,
                    Name = companyDTO.name,
                    EmailConfirmed = true
                };

                var CompanyData = companyDTO;

                var result = await _user.CreateAsync(company, companyDTO.Password);
                var roleResult = await _user.AddToRoleAsync(company, AuthenticationData.CompanyRole);

                if (!result.Succeeded || !roleResult.Succeeded || !roleResult.Succeeded && !result.Succeeded)
                {
                    return BadRequest();
                }
                await _companyInterface.CreateCompany(CompanyData);
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
                var companyDTO = await _companyInterface.GetCompanyByEmail(authentication.Email);
                if (companyDTO == null)
                {
                    return Unauthorized();
                }

                var credentials = GetSigninCredentials();
                var claims = await GetClaims(companyDTO);

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
                    companyDTO = new CompanyDTO
                    {
                        name = companyDTO.name,
                        CompanyId = companyDTO.CompanyId,
                        email = companyDTO.email
                    }
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [HttpGet("{CompanyId}")]
        public async Task<IActionResult> GetCompany(int? CompanyId)
        {
            if(CompanyId == null)
            {
                return BadRequest();
            }

            var companyData = await _companyInterface.GetCompany(CompanyId.Value);
            if(companyData == null)
            {
                return NotFound();
            }
            return Ok(companyData);
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyInterface.GetAllCompanies();
            return Ok(companies);
        }

        [AllowAnonymous]
        [HttpGet("nip/{nip}")]
        public async Task<IActionResult> CheckNIP(string nip)
        {
            var companyData = await _companyInterface.IsNipUnique(nip);
            return Ok(companyData);
        }


        [Authorize(Roles = "Company")]
        [HttpPut("update/{companyId}")]
        public async Task<ActionResult<CompanyDTO>> UpdateCompany(int? companyId, CompanyDTO companyDTO)
        {
            if (companyId == null)
            {
                return BadRequest();
            }
            companyDTO = await _companyInterface.UpdateCompany(companyId.Value, companyDTO);

            if (companyDTO == null)
            {
                return NotFound();
            }

            var user = await _user.FindByEmailAsync(companyDTO.email);
            if (user == null)
            {
                return NotFound();
            }
            var tokenPassword = await _user.GeneratePasswordResetTokenAsync(user);
            var resultPassword = await _user.ResetPasswordAsync(user, tokenPassword, companyDTO.Password);

            if (resultPassword == null)
            {
                return BadRequest();
            }

            return Ok(companyDTO);
        }

        private SigningCredentials GetSigninCredentials()
        {
            var token = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_security.Key));
            return new SigningCredentials(token, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(CompanyDTO companyDTO)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,companyDTO.email),
                new Claim(ClaimTypes.Email,companyDTO.email),
                new Claim("CompanyId", companyDTO.CompanyId.ToString())
            };

            var roles = await _user.GetRolesAsync(await _user.FindByEmailAsync(companyDTO.email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
