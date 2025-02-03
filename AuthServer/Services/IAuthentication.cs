using AuthServer.Models;
using AuthServer.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthServer.Services;

public interface IAuthentication
{
    Task<IdentityResult> Register(CreateUserDTO request);
    Task<string?> Login(LoginUserDTO request);
    Task Logout(ClaimsPrincipal user);
}