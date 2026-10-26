using System.ComponentModel.DataAnnotations;

namespace CadastroPessoas.Api.Models;

public class Telefone
{
    public int Id { get; set; }

    [EnumDataType(typeof(TipoTelefone), ErrorMessage = "Tipo inválido. Use Celular, Residencial ou Comercial.")]
    public TipoTelefone Tipo { get; set; }

    [Required(ErrorMessage = "O número é obrigatório.")]
    [StringLength(11, MinimumLength = 10, ErrorMessage = "O número deve ter 10 ou 11 dígitos, com DDD.")]
    public string Numero { get; set; } = string.Empty;
}