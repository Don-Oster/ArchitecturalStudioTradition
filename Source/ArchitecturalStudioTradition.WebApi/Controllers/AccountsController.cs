using ArchitecturalStudioTradition.Application.Account.Login;
using ArchitecturalStudioTradition.Application.Account.Register;
using ArchitecturalStudioTradition.Contract.v1.Account;
using ArchitecturalStudioTradition.CQRS;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace CompanyEmployees.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public AccountsController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        [SwaggerResponse(200, typeof(RegistrationResponse), Description = "Registers the new user", IsNullable = true)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterCommand command)
        {
            var response = await _commandBus.SendAsync(command);
            return Ok(response);
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        [SwaggerResponse(200, typeof(AuthenticationResponse), Description = "Logins the user", IsNullable = true)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var response = await _commandBus.SendAsync(command);
            return Ok(response);
        }

        [HttpPost("external-login")]
        [ValidateAntiForgeryToken]
        [SwaggerResponse(200, typeof(AuthenticationResponse), Description = "Logins the user externally", IsNullable = true)]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginCommand command)
        {
            var response = await _commandBus.SendAsync(command);
            return Ok(response);
        }

        [HttpPost("forgot-password")]
        [SwaggerResponse(200, typeof(string), Description = "Forgot password", IsNullable = true)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            await _commandBus.SendAsync(command);
            return Ok();
        }

        [HttpPost("reset-password")]
        [SwaggerResponse(200, typeof(bool), Description = "Reset password", IsNullable = true)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            await _commandBus.SendAsync(command);
            return Ok();
        }

        [HttpPost("two-step-verification")]
        [SwaggerResponse(200, typeof(TokenResponse), Description = "Two-step verification", IsNullable = true)]
        public async Task<IActionResult> TwoStepVerification([FromBody] TwoStepVerificationCommand command)
        {
            var response = await _commandBus.SendAsync(command);
            return Ok(response);
        }

        [HttpGet("email-confirmation")]
        [SwaggerResponse(200, typeof(bool), Description = "Email confirmation", IsNullable = true)]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            await _commandBus.SendAsync(new EmailConfirmationCommand { Email = email, Token = token });
            return Ok();
        }
    }
}
