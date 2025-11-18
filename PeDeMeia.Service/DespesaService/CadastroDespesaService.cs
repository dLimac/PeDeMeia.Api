using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Repository;

namespace PeDeMeia.Service.DespesaService
{
    public class CadastroDespesaService
    {
        public bool CadastroDespesa(DespesaInputModel model)
        {
            DespesaEntity despesa = new DespesaEntity(0, model.Descricao, model.Categoria, model.Valor, model.PessoaId, model.DataDespesa, model.ContaBancariaId);
            bool resultado = new DespesaRepository().Cadastrar(despesa);
            return resultado;
        }
    }
}
