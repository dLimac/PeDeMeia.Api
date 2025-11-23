using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Repository;

namespace PeDeMeia.Service
{
    public class DespesaService
    {
        private readonly DespesaRepository _despesaRepository;

        public DespesaService(DespesaRepository despesaRepository)
        {
            _despesaRepository = despesaRepository;
        }

        public List<DespesaEntity> BuscarTodos()
        {
            return _despesaRepository.BuscarTodos();
        }

        public DespesaEntity BuscarPorId(int id)
        {
            return _despesaRepository.BuscarPorId(id);
        }

        public int Cadastrar(DespesaInputModel inputModel)
        {
            var despesa = new DespesaEntity
            {
                Descricao = inputModel.Descricao,
                Categoria = inputModel.Categoria,
                Valor = inputModel.Valor,
                DataDespesa = inputModel.DataDespesa,
                PessoaId = inputModel.PessoaId,
                BancoId = inputModel.BancoId
            };

            return _despesaRepository.Cadastrar(despesa);
        }

        public bool Atualizar(int id, DespesaInputModel inputModel)
        {
            var despesa = new DespesaEntity
            {
                Descricao = inputModel.Descricao,
                Categoria = inputModel.Categoria,
                Valor = inputModel.Valor,
                DataDespesa = inputModel.DataDespesa,
                PessoaId = inputModel.PessoaId,
                BancoId = inputModel.BancoId
            };

            return _despesaRepository.Atualizar(id, despesa);
        }

        public bool Deletar(int id)
        {
            return _despesaRepository.Deletar(id);
        }

        public List<DespesaEntity> BuscarPorPessoa(int pessoaId)
        {
            return _despesaRepository.BuscarPorPessoa(pessoaId);
        }
    }
}