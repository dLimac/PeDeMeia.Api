using Microsoft.AspNetCore.Mvc;
using PeDeMeia.API.FluentValidation;
using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Service;

namespace PeDeMeia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DespesaController : ControllerBase
    {
        private readonly DespesaService _despesaService;

        public DespesaController(DespesaService despesaService)
        {
            _despesaService = despesaService;
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var despesas = _despesaService.BuscarTodos();
            return Ok(despesas);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var despesa = _despesaService.BuscarPorId(id);
            if (despesa == null)
                return NotFound();

            return Ok(despesa);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public IActionResult BuscarPorPessoa(int pessoaId)
        {
            var despesas = _despesaService.BuscarPorPessoa(pessoaId);
            return Ok(despesas);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] DespesaInputModel inputModel)
        {
            var validator = new DespesaValidator();
            var validationResult = validator.Validate(inputModel);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var id = _despesaService.Cadastrar(inputModel);
            return CreatedAtAction(nameof(BuscarPorId), new { id }, null);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] DespesaInputModel inputModel)
        {
            var validator = new DespesaValidator();
            var validationResult = validator.Validate(inputModel);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = _despesaService.Atualizar(id, inputModel);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var result = _despesaService.Deletar(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}