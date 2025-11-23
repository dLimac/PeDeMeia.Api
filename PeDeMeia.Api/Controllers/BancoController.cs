using Microsoft.AspNetCore.Mvc;
using PeDeMeia.API.FluentValidation;
using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Service;

namespace PeDeMeia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BancoController : ControllerBase
    {
        private readonly BancoService _bancoService;

        public BancoController(BancoService bancoService)
        {
            _bancoService = bancoService;
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var bancos = _bancoService.BuscarTodos();
            return Ok(bancos);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var banco = _bancoService.BuscarPorId(id);
            if (banco == null)
                return NotFound();

            return Ok(banco);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public IActionResult BuscarPorPessoa(int pessoaId)
        {
            var bancos = _bancoService.BuscarPorPessoa(pessoaId);
            return Ok(bancos);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] BancoInputModel inputModel)
        {
            var validator = new BancoValidator();
            var validationResult = validator.Validate(inputModel);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var id = _bancoService.Cadastrar(inputModel);
            return CreatedAtAction(nameof(BuscarPorId), new { id }, null);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] BancoInputModel inputModel)
        {
            var validator = new BancoValidator();
            var validationResult = validator.Validate(inputModel);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = _bancoService.Atualizar(id, inputModel);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var result = _bancoService.Deletar(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}