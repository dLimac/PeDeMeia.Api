using Microsoft.AspNetCore.Mvc;
using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Service.DespesaService;

namespace PeDeMeia.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DespesaController : ControllerBase
    {    
        
        [HttpPost("/despesa/cadastro")]
        public bool CadastrarDespesa(DespesaInputModel model)
            => new CadastroDespesaService().CadastroDespesa(model);

        [HttpGet("/despesas")]
        public DespesaEntity ObterTodos()
            => new BuscaDespesaService().ObterTodos();

        [HttpGet("/despesa")]
        public DespesaEntity ObterPorCategoria(string categoria)
            => new BuscaDespesaService().ObterPorCategoria(categoria);
    }
}
