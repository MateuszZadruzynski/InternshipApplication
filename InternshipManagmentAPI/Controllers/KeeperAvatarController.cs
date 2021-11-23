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
    public class KeeperAvatarController : Controller
    {
        private readonly KeeperAvatarsInterface _keeperAvatarInterface;
        public KeeperAvatarController(KeeperAvatarsInterface keeperAvatarInterface)
        {
            _keeperAvatarInterface = keeperAvatarInterface;
        }

        [Authorize(Roles = "Keeper")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateKeeperAvatar(KeeperAvatarsDTO keeperAvatarDTO)
        {
            if (keeperAvatarDTO == null)
            {
                return BadRequest();
            }

            var AvatarsData = await _keeperAvatarInterface.CreateAvatar(keeperAvatarDTO);
            if (AvatarsData == null)
            {
                return NotFound();
            }
            return Ok(AvatarsData);
        }

        [Authorize(Roles = "Keeper")]
        [HttpDelete("delete/{keeperId}")]
        public async Task<IActionResult> DeleteAvatar(int? keeperId)
        {
            if (keeperId == null)
            {
                return BadRequest();
            }

            var data = await _keeperAvatarInterface.DeleteAvatarByKeeperId(keeperId.Value);
            return Ok(data);
        }

        [Authorize(Roles = "Keeper,Company,Student")]
        [HttpGet("avatar/{keeperId}")]
        public async Task<IActionResult> GetKeeperAvatar(int? keeperId)
        {
            var diaryData = await _keeperAvatarInterface.GetKeeperAvatar(keeperId.Value);
            return Ok(diaryData);
        }
    }
}
