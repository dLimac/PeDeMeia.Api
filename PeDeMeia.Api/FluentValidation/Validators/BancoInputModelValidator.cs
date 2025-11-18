using FluentValidation;
using PeDeMeia.Domain.DTOs.InputModel;

namespace PeDeMeia.Api.FluentValidation.Validators
{
    public class BancoInputModelValidator : AbstractValidator<BancoInputModel>
    {
        public BancoInputModelValidator() 
        {
            RuleFor(x => x.Nome)
                    .NotEmpty().WithMessage("Nome do banco é obrigatório")
                    .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Saldo)
                .GreaterThanOrEqualTo(0).WithMessage("Saldo não pode ser negativo");

            RuleFor(x => x.PessoaId)
                .GreaterThan(0).WithMessage("ID da pessoa inválido");
        }
    }
}
