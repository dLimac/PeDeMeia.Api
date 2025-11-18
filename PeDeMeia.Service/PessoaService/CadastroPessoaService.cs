using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Repository;

namespace PeDeMeia.Service.PessoaService
{
    public class CadastroPessoaService
    {
        public bool CadastroPessoa(PessoaInputModel model)
        {
            PessoaEntity pessoa = new PessoaEntity(0, model.Nome, model.Cpf, model.Conjuge, model.Saldo);
            bool resultado = new PessoaRepository().Cadastrar(pessoa);
            return resultado;
        }
    }
}
