using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Repository;

namespace PeDeMeia.Service.BancoService
{
    public class CadastroBancoService
    {
        public bool CadastroBanco(BancoInputModel model)
        {
            BancoEntity banco = new BancoEntity(0, model.Nome, model.Saldo, model.PessoaId);
            bool resultado = new BancoRepository().Cadastrar(banco);
            return resultado;
        }
    }
}
