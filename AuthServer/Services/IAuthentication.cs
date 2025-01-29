using AuthServer.Models;
using AuthServer.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Services;

public interface IAuthentication
{
    Task<IdentityResult> Register(CreateUserDTO request);
    Task<string?> Login(LoginUserDTO request);
    Task<IdentityResult> RegisterPaciente(RegisterPacienteDTO request);
    Task<IdentityResult> RegisterMedico(RegisterMedicoDTO request);
    Task<Paciente> GetPacienteById(string id);
    Task<IEnumerable<Paciente>> GetAllPacientes();
}