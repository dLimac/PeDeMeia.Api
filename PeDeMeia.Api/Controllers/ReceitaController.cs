using Microsoft.AspNetCore.Mvc;
using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Service.ReceitaService;

namespace PeDeMeia.Api.Controllers
{
    public class ReceitaController : ControllerBase
    {

        [HttpPost("/receita/cadastro")]
        public bool CadastrarReceita(ReceitaInputModel model)
            => new CadastroReceitaService().CadastroReceita(model);

        [HttpGet("/receitas")]
        public ReceitaEntity ObterTodos()
            => new BuscaReceitaService().ObterTodos();

        [HttpGet("/receita")]
        public ReceitaEntity ObterPorCategoria(string categoria)
            => new BuscaReceitaService().ObterPorCategoria(categoria);
    }
}
