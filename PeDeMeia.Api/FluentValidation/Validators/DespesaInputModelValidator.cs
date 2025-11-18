using FluentValidation;
using PeDeMeia.Domain.DTOs.InputModel;

namespace PeDeMeia.Api.FluentValidation.Validators
{
    public class DespesaInputModelValidator : AbstractValidator<DespesaInputModel>
    {
        public DespesaInputModelValidator()
        {
            RuleFor(x => x.Descricao)
                    .NotEmpty().WithMessage("Descrição é obrigatória")
                    .MaximumLength(200).WithMessage("Descrição deve ter no máximo 200 caracteres");

            RuleFor(x => x.Categoria)
                .NotEmpty().WithMessage("Categoria é obrigatória")
                .MaximumLength(50).WithMessage("Categoria deve ter no máximo 50 caracteres");

            RuleFor(x => x.Valor)
                .GreaterThan(0).WithMessage("Valor deve ser maior que zero");

            RuleFor(x => x.DataDespesa)
                .NotEmpty().WithMessage("Data da despesa é obrigatória");

            RuleFor(x => x.PessoaId)
                .GreaterThan(0).WithMessage("ID da pessoa inválido");
        }
    }
}
