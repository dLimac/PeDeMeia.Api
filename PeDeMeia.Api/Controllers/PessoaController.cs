using Microsoft.AspNetCore.Mvc;
using PeDeMeia.API.FluentValidation;
using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Service;

namespace PeDeMeia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaService _pessoaService;

        public PessoaController(PessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var pessoas = _pessoaService.BuscarTodos();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var pessoa = _pessoaService.BuscarPorId(id);
            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] PessoaInputModel inputModel)
        {
            var validator = new PessoaValidator();
            var validationResult = validator.Validate(inputModel);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var id = _pessoaService.Cadastrar(inputModel);
            return CreatedAtAction(nameof(BuscarPorId), new { id }, null);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] PessoaInputModel inputModel)
        {
            var validator = new PessoaValidator();
            var validationResult = validator.Validate(inputModel);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = _pessoaService.Atualizar(id, inputModel);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var result = _pessoaService.Deletar(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}