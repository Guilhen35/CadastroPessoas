# CadastroPessoas

API REST em .NET 10 para cadastro de pessoas e seus telefones (relação 1 para N), com os dados guardados em memória.

Projeto desenvolvido como teste técnico para vaga de Desenvolvedor Júnior .NET.

## Tecnologias

- C# e .NET 10 (LTS)
- ASP.NET Core Web API com Controllers
- Data Annotations para validação
- OpenAPI para documentação das rotas
- Git e GitHub para versionamento

## Como rodar

Pré-requisito: ter o .NET 10 SDK instalado.

```bash
git clone https://github.com/Guilhen35/CadastroPessoas.git
cd CadastroPessoas/CadastroPessoas.Api
dotnet run
```

A API sobe em `http://localhost:5297`.

Para testar, abra o arquivo `CadastroPessoas.Api.http` no Visual Studio e clique em **Enviar solicitação** em cada teste.

> Os dados ficam em memória: ao reiniciar a API, tudo é apagado.

## Rotas

### Pessoas

| Verbo | Rota | O que faz |
|---|---|---|
| GET | `/api/pessoas` | Lista todas as pessoas |
| GET | `/api/pessoas/{id}` | Busca uma pessoa pelo id |
| POST | `/api/pessoas` | Cadastra uma pessoa (pode vir com telefones) |
| PUT | `/api/pessoas/{id}` | Atualiza os dados da pessoa |
| DELETE | `/api/pessoas/{id}` | Remove a pessoa |

### Telefones

| Verbo | Rota | O que faz |
|---|---|---|
| GET | `/api/pessoas/{pessoaId}/telefones` | Lista os telefones da pessoa |
| GET | `/api/pessoas/{pessoaId}/telefones/{telefoneId}` | Busca um telefone da pessoa |
| POST | `/api/pessoas/{pessoaId}/telefones` | Adiciona um telefone à pessoa |
| PUT | `/api/pessoas/{pessoaId}/telefones/{telefoneId}` | Atualiza um telefone |
| DELETE | `/api/pessoas/{pessoaId}/telefones/{telefoneId}` | Remove um telefone |

> O PUT da pessoa não altera os telefones. Eles são gerenciados pelas rotas próprias.

Respostas usadas: `200 OK`, `201 Created`, `204 No Content`, `400 Bad Request` (dados inválidos) e `404 Not Found`.

## Exemplo

Cadastrar uma pessoa com telefones (`POST /api/pessoas`):

```json
{
  "nome": "Carlos Lima",
  "cpf": "11122233344",
  "dataNascimento": "1980-07-10",
  "ativo": true,
  "telefones": [
    { "tipo": "Celular", "numero": "43999998888" },
    { "tipo": "Comercial", "numero": "4332551234" }
  ]
}
```

O campo `tipo` aceita `Celular`, `Residencial` ou `Comercial`. Os ids são gerados pela API.

## Decisões técnicas

- **Controllers em vez de Minimal APIs:** organiza as rotas em uma classe, deixando o código mais fácil de ler.
- **Dados em memória:** o desafio pede persistência mínima em memória, então os dados ficam em uma lista estática enquanto a API está rodando.
- **Validação com Data Annotations:** o `[ApiController]` devolve `400` sozinho quando algo está inválido.
- **Rotas de telefone aninhadas:** o telefone sempre pertence a uma pessoa e é procurado dentro da pessoa da URL.
- **CPF como texto:** para não perder os zeros à esquerda. CPF não é usado em contas.

## Limitações e melhorias futuras

- **Sem login:** qualquer pessoa pode cadastrar, editar ou excluir. Próximo passo: adicionar autenticação.
- **Dados pessoais expostos:** CPF e telefones aparecem nas consultas, o que exige cuidado com a LGPD.
- **Dados em memória:** somem ao reiniciar a API. Próximo passo: usar um banco de dados.
- **CPF sem validação completa:** não confere os dígitos verificadores e pode ser repetido.
- **Código repetido:** a busca da pessoa se repete em vários métodos. Poderia virar um método só ou uma camada de serviço.
- **Testes manuais:** hoje os testes são feitos pelo arquivo `.http`. Próximo passo: testes automatizados.