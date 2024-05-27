using Data.EFCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly SignInManager<PessoaComAcesso> _signInManager;
    private readonly UserManager<PessoaComAcesso> _userManager;

    public AuthController(SignInManager<PessoaComAcesso> signInManager, UserManager<PessoaComAcesso> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    [Route("AccessDenied")]
    public IActionResult AccessDenied()
    {
        Response.StatusCode = 403;
        return Content("Acesso negado. Esta rota só pode ser acessada por administradores.");
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Unauthorized(new { message = "Usuário ou senha inválidos." });
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);

        if (result.Succeeded)
            return Ok(new { message = "Login bem-sucedido."  });

        return Unauthorized(new { message = "Usuário ou senha inválidos." });
    }

    [HttpPost]
    [Route("Logout")]


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { message = "Logout bem-sucedido." });
    }

    [HttpPost]
    [Route("ChangePassword")]
    public async Task<IActionResult> ChangePassword(string email, string oldPassword, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return NotFound(new { message = "Usuário não encontrado." });
        }

        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (result.Succeeded)
            return Ok(new { message = "Senha alterada." });

        return BadRequest(new { message = "Erro ao alterar." });
    }

    [HttpPost]
    [Route("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return NotFound(new { message = "Usuário não encontrado." });
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        return Ok(new { message = "{}" });
    }

}
