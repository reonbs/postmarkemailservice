using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Postmark.Email.Model.Models;
using Postmark.Email.Service.Interfaces;

namespace Postmark.Email.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        #region declares
        private readonly IEmailService _emailService;
        #endregion

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("single")]
        public async Task<IActionResult> Single([FromBody] EmailRequest emailRequest)
        {
            var response =  await _emailService.PublishSingleEmail(emailRequest);
            return StatusCode(response.Code, response);
        }

        [HttpPost("batch")]
        public async Task<IActionResult> Single([FromBody] List<EmailRequest> emailRequests)
        {
            var response = await _emailService.PublishBatchEmail(emailRequests);
            return StatusCode(response.Code, response);
        }
    }
}
