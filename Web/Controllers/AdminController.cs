using Data.EFCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = "AdminPolicy")]
public class AdminController : Controller
{
    private readonly UserManager<PessoaComAcesso> _userManager;

    public AdminController(UserManager<PessoaComAcesso> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [Route("GetUsers")]
    public IActionResult GetUsers()
    {
        return Ok(_userManager.Users);
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(string email, string password)
    {
        var user = new PessoaComAcesso
        {
            UserName = email, 
            Email = email
        };
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return Ok(new { message = "Usuário registrado." });
        }

        return BadRequest(new { message = "Erro ao criar usuário." });
    }

    [HttpDelete]
    [Route("DeleteUser")]
    public async Task<IActionResult> Delete(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return NotFound(new { message = "Usuário não encontrado." });
        }

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return Ok(new { message = "Usuário deletado." });
        }

        return BadRequest(new { message = "Erro ao deletar usuário." });
    }
}
