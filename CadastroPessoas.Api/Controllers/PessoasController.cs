using CadastroPessoas.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroPessoas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    // Dados de memória temporários
    private static readonly List<Pessoa> _pessoas = new();
    private static int _proximoId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<Pessoa>> ObterTodas()
    {
        return Ok(_pessoas);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Pessoa> ObterPorId(int id)
    {
        var pessoa = _pessoas.FirstOrDefault(p => p.Id == id);

        if (pessoa is null)
            return NotFound();

        return Ok(pessoa);
    }

    [HttpPost]
    public ActionResult<Pessoa> Criar(Pessoa pessoa)
    {
        pessoa.Id = _proximoId++;
        _pessoas.Add(pessoa);

        return CreatedAtAction(nameof(ObterPorId), new { id = pessoa.Id }, pessoa);
    }
}