using Microsoft.AspNetCore.Mvc;
using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Service.BancoService;

namespace PeDeMeia.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BancoController : ControllerBase
    {
        [HttpPost("/banco/cadastro")]
        public bool CadastrarBanco (BancoInputModel model)
            => new CadastroBancoService().CadastroBanco(model);
    }
}
