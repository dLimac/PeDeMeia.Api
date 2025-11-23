using FluentValidation;
using PeDeMeia.Domain.DTOs.InputModel;

namespace PeDeMeia.API.FluentValidation
{
    public class PessoaValidator : AbstractValidator<PessoaInputModel>
    {
        public PessoaValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("CPF é obrigatório")
                .Length(11, 14).WithMessage("CPF inválido");

            RuleFor(x => x.Saldo)
                .GreaterThanOrEqualTo(0).WithMessage("Saldo não pode ser negativo");
        }
    }
}