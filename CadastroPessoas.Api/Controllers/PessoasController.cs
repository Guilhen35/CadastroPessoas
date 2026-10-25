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

    [HttpPut("{id:int}")]
    public IActionResult Atualizar(int id, Pessoa pessoaAtualizada)
    {
        var pessoa = _pessoas.FirstOrDefault(p => p.Id == id);

        if (pessoa is null)
            return NotFound();

        pessoa.Nome = pessoaAtualizada.Nome;
        pessoa.Cpf = pessoaAtualizada.Cpf;
        pessoa.DataNascimento = pessoaAtualizada.DataNascimento;
        pessoa.Ativo = pessoaAtualizada.Ativo;

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Remover(int id)
    {
        var pessoa = _pessoas.FirstOrDefault(p => p.Id == id);

        if (pessoa is null)
            return NotFound();

        _pessoas.Remove(pessoa);

        return NoContent();
    }

}