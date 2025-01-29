using AuthServer.Models.DTO;
using AuthServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        public AccountController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [Authorize]
        [HttpGet("pacientes")]
        public async Task<IActionResult> GetPacientes()
        {
            var pacientes = await _authentication.GetAllPacientes();
            return Ok(pacientes);
        }

        [HttpGet("pacientes/{id}")]
        public async Task<IActionResult> GetPacienteById(string id)
        {
            var paciente = await _authentication.GetPacienteById(id);
            if (paciente == null)
                return NotFound("Paciente não encontrado.");

            return Ok(paciente);
        }
        [HttpPost("register-medico")]
        public async Task<IActionResult> RegisterMedico([FromBody] RegisterMedicoDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authentication.RegisterMedico(request);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Médico registrado com sucesso!");
        }

        [HttpPost("register-paciente")]
        public async Task<IActionResult> RegisterPaciente([FromBody] RegisterPacienteDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authentication.RegisterPaciente(request);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Paciente registrado com sucesso!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO request)
        {
            try
            {
                var token = await _authentication.Login(request);
                return Ok(new { access_token = token });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message); 
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return Ok(new { message = "Logout realizado com sucesso!" });
        }
    }
}
