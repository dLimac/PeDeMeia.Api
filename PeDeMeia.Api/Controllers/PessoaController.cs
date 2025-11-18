using Microsoft.AspNetCore.Mvc;
using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Service.PessoaService;


namespace PeDeMeia.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        [HttpPost("/user/cadastro")]
        public bool CadastrarPessoa(PessoaInputModel model)
           => new CadastroPessoaService().CadastroPessoa(model);
    }
}
