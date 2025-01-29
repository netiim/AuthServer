using System.ComponentModel.DataAnnotations;

namespace AuthServer.Models.DTO
{
    public class RegisterPacienteDTO
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}