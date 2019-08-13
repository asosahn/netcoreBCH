using Microsoft.AspNetCore.Mvc;
using test2.Entities;
using test2.Services;

namespace test2.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;
        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }
        [HttpPost]
        public ActionResult<string> SendEmail([FromBody] Email email)
        {
            var email_ = emailService.SendEmail(email);
            return Ok(email_);
        }
    }
}