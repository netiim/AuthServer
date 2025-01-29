using Microsoft.AspNetCore.Identity;

namespace AuthServer.Services;

public class IdentityRoles
{
    public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in GetAllRoles())
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    public static List<string> GetAllRoles()
    {
        return new List<string>
        {
            nameof(UserRoles.Medico),
            nameof(UserRoles.Paciente)
        };
    }
}
public enum UserRoles
{
    Medico,
    Paciente
}
