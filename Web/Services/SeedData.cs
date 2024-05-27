using Microsoft.AspNetCore.Identity;
using Data.EFCore.Models;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<PessoaComAcesso> userManager, RoleManager<PerfilDeAcesso> roleManager)
    {
        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new PerfilDeAcesso { Name = roleName });
            }
        }

        // Criar um usuário administrador
        var adminUser = new PessoaComAcesso
        {
            UserName = "samuel.dias@dadyilha.com.br",
            Email = "samuel.dias@dadyilha.com.br"
        };

        var user = await userManager.FindByEmailAsync(adminUser.Email);
        if (user == null)
        {
            var createAdmin = await userManager.CreateAsync(adminUser, "Qwerty@123");
            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}