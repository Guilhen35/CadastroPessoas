
namespace CadastroPessoas.Api.Models;

public class Pessoa
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set;  }
    public bool Ativo { get; set; } = true;
}