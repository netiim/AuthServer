using AuthServer.Models;
using AuthServer.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static AuthServer.Services.IdentityRoles;

namespace AuthServer.Services
{
    public class AuthenticationService : IAuthentication
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;
        private ApplicationDbContext DbContext { get; set; }

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenService jwtTokenService,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            DbContext = dbContext;
        }

        public async Task<IdentityResult> Register(CreateUserDTO request)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            return result;
        }
        public async Task<IdentityResult> RegisterPaciente(RegisterPacienteDTO request)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) return result;

            await _userManager.AddToRoleAsync(user, nameof(UserRoles.Paciente));

            var paciente = new Paciente
            {
                Id = user.Id,
                DataNascimento = request.DataNascimento,
                User = user
            };

            DbContext.Pacientes.Add(paciente);
            await DbContext.SaveChangesAsync();

            return result;
        }
        public async Task<string?> Login(LoginUserDTO request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) throw new Exception("Usuário ou senha inválidos!");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: true);
            if (result.IsLockedOut) throw new Exception("Usuário bloqueado! Tente novamente mais tarde.");
            if (!result.Succeeded) throw new Exception("Usuário ou senha inválidos!");

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenService.GenerateToken(user.Id, user.UserName, roles);

            return token;
        }
        public async Task<IEnumerable<Paciente>> GetAllPacientes()
        {
            return await DbContext.Pacientes.Include(p => p.User).ToListAsync();
        }

        public async Task<Paciente> GetPacienteById(string id)
        {
            return await DbContext.Pacientes.Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public Task<IdentityResult> RegisterMedico(RegisterMedicoDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
