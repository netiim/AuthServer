using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Models
{
    public class ApplicationUser : IdentityUser 
    {
    }
    public class Medico
    {
        [Key]
        public string Id { get; set; } 
        public string CRM { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class Paciente
    {
        [Key]
        public string Id { get; set; } 
        public DateTime DataNascimento { get; set; }
        public ApplicationUser User { get; set; }
    }
}
