using CadastroPessoas.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroPessoas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    
    private static readonly List<Pessoa> _pessoas = new();
    private static int _proximoId = 1;
    private static int _proximoTelefoneId = 1;

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
        if (pessoa.DataNascimento > DateOnly.FromDateTime(DateTime.Today))
            return BadRequest("A data de nascimento não pode ser no futuro.");

        pessoa.Id = _proximoId++;

        foreach (var telefone in pessoa.Telefones)
            telefone.Id = _proximoTelefoneId++;

        _pessoas.Add(pessoa);

        return CreatedAtAction(nameof(ObterPorId), new { id = pessoa.Id }, pessoa);
    }

    [HttpPut("{id:int}")]
    public IActionResult Atualizar(int id, Pessoa pessoaAtualizada)
    {
        if (pessoaAtualizada.DataNascimento > DateOnly.FromDateTime(DateTime.Today))
            return BadRequest("A data de nascimento não pode ser no futuro.");

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

    [HttpGet("{pessoaId:int}/telefones")]
    public ActionResult<IEnumerable<Telefone>> ObterTelefones(int pessoaId)
    {
        var pessoa = _pessoas.FirstOrDefault(p => p.Id == pessoaId);

        if (pessoa is null)
            return NotFound();

        return Ok(pessoa.Telefones);
    }

    [HttpGet("{pessoaId:int}/telefones/{telefoneId:int}")]
    public ActionResult<Telefone> ObterTelefonePorId(int pessoaId, int telefoneId)
    {
        var pessoa = _pessoas.FirstOrDefault(p => p.Id == pessoaId);

        if (pessoa is null)
            return NotFound();

        var telefone = pessoa.Telefones.FirstOrDefault(t => t.Id == telefoneId);

        if (telefone is null)
            return NotFound();

        return Ok(telefone);
    }

    [HttpPost("{pessoaId:int}/telefones")]
    public ActionResult<Telefone> AdicionarTelefone(int pessoaId, Telefone telefone)
    {
        var pessoa = _pessoas.FirstOrDefault(p => p.Id == pessoaId);

        if (pessoa is null)
            return NotFound();

        telefone.Id = _proximoTelefoneId++;
        pessoa.Telefones.Add(telefone);

        return CreatedAtAction(nameof(ObterTelefonePorId), new { pessoaId = pessoa.Id, telefoneId = telefone.Id }, telefone);
    }

    [HttpPut("{pessoaId:int}/telefones/{telefoneId:int}")]
    public IActionResult AtualizarTelefone(int pessoaId, int telefoneId, Telefone telefoneAtualizado)
    {
        var pessoa = _pessoas.FirstOrDefault(p => p.Id == pessoaId);

        if (pessoa is null)
            return NotFound();

        var telefone = pessoa.Telefones.FirstOrDefault(t => t.Id == telefoneId);

        if (telefone is null)
            return NotFound();

        telefone.Tipo = telefoneAtualizado.Tipo;
        telefone.Numero = telefoneAtualizado.Numero;

        return NoContent();
    }

    [HttpDelete("{pessoaId:int}/telefones/{telefoneId:int}")]
    public IActionResult RemoverTelefone(int pessoaId, int telefoneId)
    {
        var pessoa = _pessoas.FirstOrDefault(p => p.Id == pessoaId);

        if (pessoa is null)
            return NotFound();

        var telefone = pessoa.Telefones.FirstOrDefault(t => t.Id == telefoneId);

        if (telefone is null)
            return NotFound();

        pessoa.Telefones.Remove(telefone);

        return NoContent();
    }
}