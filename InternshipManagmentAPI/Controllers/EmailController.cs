using InternshipApplicationServer.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace InternshipManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : Controller
    {
        private readonly EmailInterface _emailInterface;

        public EmailController(EmailInterface emailInterface)
        {
            _emailInterface = emailInterface;
        }

        [AllowAnonymous]
        [HttpPost("send")]
        public void EmailSend(Email emailData)
        {
            _emailInterface.Send(emailData);
        }

    }
}
