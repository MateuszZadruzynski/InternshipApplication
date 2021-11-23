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
    public class KeeperController : Controller
    {
        private readonly KeeperInterface _keeperInterface;
        private readonly UserManager<User> _user;
        private readonly SignInManager<User> _signIn;
        private readonly Security _security;

        public KeeperController(UserManager<User> user, SignInManager<User> signIn,
            KeeperInterface keeperInterface, IOptions<Security> options)
        {
            _user = user;
            _signIn = signIn;
            _keeperInterface = keeperInterface;
            _security = options.Value;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SingUp([FromBody] KeeperDTO keeperDTO)
        {
            if (ModelState.IsValid)
            {
                var keeper = new User()
                {
                    UserName = keeperDTO.email,
                    Email = keeperDTO.email,
                    Name = keeperDTO.lastName,
                    EmailConfirmed = true
                };

                var result = await _user.CreateAsync(keeper, keeperDTO.Password);
                var roleResult = await _user.AddToRoleAsync(keeper, AuthenticationData.KeeperRole);

                if (!result.Succeeded || !roleResult.Succeeded || !roleResult.Succeeded && !result.Succeeded)
                {
                    return BadRequest();
                }
                await _keeperInterface.CreateKeeper(keeperDTO);
                return StatusCode(201);
            }
            else
            {
                return BadRequest();
            }

        }

        [Authorize(Roles = "Company")]
        [HttpDelete("delete/{keeperId}")]
        public async Task<IActionResult> DeleteKeeper(int? keeperId)
        {
            if (keeperId == null)
            {
                return BadRequest();
            }

            var keeperData = await _keeperInterface.DeleteKeeper(keeperId.Value);
            return Ok(keeperData);
        }

        [AllowAnonymous]
        [HttpPost("singin")]
        public async Task<IActionResult> SingIn([FromBody] AuthenticationUser authentication)
        {
            var result = await _signIn.PasswordSignInAsync(authentication.Email,
                authentication.Password, false, false);

            if (result.Succeeded)
            {
                var keeperDTO = await _keeperInterface.GetKeeperByEmail(authentication.Email);
                if (keeperDTO == null)
                {
                    return Unauthorized();
                }

                var credentials = GetSigninCredentials();
                var claims = await GetClaims(keeperDTO);

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
                    keeperDTO = new KeeperDTO
                    {
                        lastName = keeperDTO.lastName,
                        KeeperId = keeperDTO.KeeperId,
                        email = keeperDTO.email
                    }
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize(Roles = "Keeper,Company,Student")]
        [HttpGet("{keeperId}")]
        public async Task<IActionResult> GetKeeper(int? keeperId)
        {
            if (keeperId == null)
            {
                return BadRequest();
            }

            var keeperData = await _keeperInterface.GetKeeper(keeperId.Value);
            if (keeperData == null)
            {
                return NotFound();
            }
            return Ok(keeperData);
        }

        [Authorize(Roles = "Keeper,Company,Student")]
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetKeepertByEmail(string email)
        {
            if (email == null)
            {
                return BadRequest();
            }

            var keeperData = await _keeperInterface.GetKeeperByEmail(email);
            if (keeperData == null)
            {
                return NotFound();
            }
            return Ok(keeperData);
        }

        [Authorize(Roles = "Keeper")]
        [HttpPut("update/{keeperId}")]
        public async Task<ActionResult<KeeperDTO>> UpdateKeeper(int? keeperId, KeeperDTO keeperDTO)
        {
            if (keeperId == null)
            {
                return BadRequest();
            }
            keeperDTO = await _keeperInterface.UpdateKeeper(keeperId.Value, keeperDTO);

            if (keeperDTO == null)
            {
                return NotFound();
            }

            var user = await _user.FindByEmailAsync(keeperDTO.email);
            if (user == null)
            {
                return NotFound();
            }
            var tokenPassword = await _user.GeneratePasswordResetTokenAsync(user);
            var resultPassword = await _user.ResetPasswordAsync(user, tokenPassword, keeperDTO.Password);

            if (resultPassword == null)
            {
                return BadRequest();
            }

            return Ok(keeperDTO);
        }

        [Authorize(Roles = "Keeper,Company,Student")]
        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetCompanyKeepers(int? companyId)
        {
            if (companyId == null)
            {
                return BadRequest();
            }

            var keeperData = await _keeperInterface.GetAllCompanyKeepers(companyId.Value);

            if (keeperData == null)
            {
                return NotFound();
            }
            return Ok(keeperData);
        }
        private SigningCredentials GetSigninCredentials()
        {
            var token = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_security.Key));
            return new SigningCredentials(token, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(KeeperDTO keeperDTO)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,keeperDTO.email),
                new Claim(ClaimTypes.Email,keeperDTO.email),
                new Claim("KeeperId", keeperDTO.KeeperId.ToString())
            };

            var roles = await _user.GetRolesAsync(await _user.FindByEmailAsync(keeperDTO.email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
