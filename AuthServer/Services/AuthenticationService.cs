using AuthServer.Models;
using AuthServer.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            var roles = Enum.GetValues<UserRoles>().Select(e => e.ToString()).ToList();
            await _userManager.AddToRolesAsync(user, roles);


            return result;
        }
        private async Task<bool> SaveTokenAsync(ApplicationUser user, string token)
        {
            try
            {
                var existingToken = await _userManager.GetAuthenticationTokenAsync(user, "JWT", "AccessToken");

                if (!string.IsNullOrEmpty(existingToken))
                {
                    // Remove o token anterior (se houver) para evitar duplicatas
                    await _userManager.RemoveAuthenticationTokenAsync(user, "JWT", "AccessToken");
                }

                // Salva o novo token
                await _userManager.SetAuthenticationTokenAsync(user, "JWT", "AccessToken", token);
                return true;
            }
            catch
            {
                return false;
            }
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

            var tokenSaved = await SaveTokenAsync(user, token);

            if (!tokenSaved) throw new Exception("Erro ao salvar o token.");

            return token;
        }

        public async Task Logout(ClaimsPrincipal userLogado)
        {
            var user = await _userManager.GetUserAsync(userLogado);

            if (user == null) throw new Exception("Ocorreu um problema para deslogar usuário não encontrado.");

            await _userManager.RemoveAuthenticationTokenAsync(user, "JWT", "AccessToken");
        }
    }
}
