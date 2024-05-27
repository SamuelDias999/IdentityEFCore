using Data.EFCore.Models;
using Data.EFCore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PessoaController : Controller
{
    private readonly DefaultRepository _repository;


    public PessoaController(DefaultRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("GetAll")]
    public ActionResult<List<Pessoa>> GetAll()
    {
        return _repository.GetAll<Pessoa>().ToList();
    }

    [HttpGet]
    [Route("GetById/{id}")]
    public ActionResult<List<Pessoa>> GetById(int id)
    {

        return _repository.RetrieveBy<Pessoa>(t => t.Id == id);
    }

    [HttpPost]
    [Route("Add")]
    public IActionResult Add(Pessoa pessoa)
    {
        _repository.Add(pessoa);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public IActionResult Delete(Pessoa pessoa)
    {
        _repository.Delete(pessoa);
        return Ok();
    }
}
