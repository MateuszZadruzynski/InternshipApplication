using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace InternshipManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadController : Controller
    {
        [Authorize(Roles = "Keeper,Student,Company")]
        [HttpPost("file")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize(Roles = "Keeper,Student,Company")]
        [HttpDelete("delete/{name}")]
        public bool Delete(string name)
        {

            bool state = false;

            var folderName = Path.Combine("StaticFiles", "Images");
            var pathToDelete = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            try
            {
                var fullPath = Path.Combine(pathToDelete, name);
                FileInfo file = new FileInfo(fullPath);
                if (file.Exists)
                {
                    file.Delete();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
