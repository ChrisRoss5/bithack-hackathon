using BddAPI.DTOs.Request;
using BddAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BddAPI.Controllers;

[ApiController]
[Route("api/email")]
public class EmailController(IEmailService emailService) : ControllerBase
{
    [HttpPost("send")]
    public async Task<IActionResult> SendEmailAsync(EmailMessage emailMessage)
    {
        await emailService.Send(emailMessage);
        return Ok();
    }
}