using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services.interfaces;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<object> SendMail([FromBody] MailRequest mailRequest)
        {
            var res = await _mailService.SendMail(mailRequest);
            if (string.Equals(res, "OK"))
                return Ok(res);
            return BadRequest(res);
        }
    }
}