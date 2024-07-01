using ImmobiliareApi.Entities;
using ImmobiliareApi.Interfaces;
using ImmobiliareApi.Interfaces.IBusinessServices;
using ImmobiliareApi.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.NetworkInformation;

namespace ImmobiliareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public RegistrationController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Logica per gestire la registrazione del cliente
            // ...

            // Inviare una notifica via email all'amministratore
            await _emailService.NotifyAdminOfRegistration(request.Name,request.LastName, request.Email);

            return Ok("Registration processed and notification sent.");
        }
    }
}
