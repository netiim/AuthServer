namespace AuthServer.Models.DTO
{
    public class RegisterMedicoDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CRM { get; set; }  // Campo obrigatório para médicos
    }
}