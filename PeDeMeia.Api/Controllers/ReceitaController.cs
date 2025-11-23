using Microsoft.AspNetCore.Mvc;
using PeDeMeia.API.FluentValidation;
using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Service;

namespace PeDeMeia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitaController : ControllerBase
    {
        private readonly ReceitaService _receitaService;

        public ReceitaController(ReceitaService receitaService)
        {
            _receitaService = receitaService;
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var receitas = _receitaService.BuscarTodos();
            return Ok(receitas);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var receita = _receitaService.BuscarPorId(id);
            if (receita == null)
                return NotFound();

            return Ok(receita);
        }

        [HttpGet("pessoa/{pessoaId}")]
        public IActionResult BuscarPorPessoa(int pessoaId)
        {
            var receitas = _receitaService.BuscarPorPessoa(pessoaId);
            return Ok(receitas);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] ReceitaInputModel inputModel)
        {
            var validator = new ReceitaValidator();
            var validationResult = validator.Validate(inputModel);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var id = _receitaService.Cadastrar(inputModel);
            return CreatedAtAction(nameof(BuscarPorId), new { id }, null);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] ReceitaInputModel inputModel)
        {
            var validator = new ReceitaValidator();
            var validationResult = validator.Validate(inputModel);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = _receitaService.Atualizar(id, inputModel);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var result = _receitaService.Deletar(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
