using ImmobiliareApi.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace ImmobiliareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendEmail(string Body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("katelynn.bruen36@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("katelynn.bruen36@ethereal.email"));
            email.Subject = "Welcome to new Job";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = Body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("katelynn.bruen36@ethereal.email", "jSWAQF8JUmVhDsrXV1");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }
    }
}
