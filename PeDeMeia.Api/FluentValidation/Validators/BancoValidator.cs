using FluentValidation;
using PeDeMeia.Domain.DTOs.InputModel;

namespace PeDeMeia.API.FluentValidation
{
    public class BancoValidator : AbstractValidator<BancoInputModel>
    {
        public BancoValidator()
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