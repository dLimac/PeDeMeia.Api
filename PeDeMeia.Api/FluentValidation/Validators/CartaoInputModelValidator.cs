using FluentValidation;
using PeDeMeia.Domain.DTOs.InputModel;

namespace PeDeMeia.Api.FluentValidation.Validators
{
    public class CartaoInputModelValidator : AbstractValidator<CartaoInputModel>
    {
        public CartaoInputModelValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome do cartão é obrigatório")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Limite)
                .GreaterThan(0).WithMessage("Limite deve ser maior que zero");

            RuleFor(x => x.ValorFatura)
                .GreaterThanOrEqualTo(0).WithMessage("Valor da fatura não pode ser negativo");

            RuleFor(x => x.DataVencimentoFatura)
                .NotEmpty().WithMessage("Data de vencimento é obrigatória");

            RuleFor(x => x.PessoaId)
                .GreaterThan(0).WithMessage("ID da pessoa inválido");
        }
    }
}
