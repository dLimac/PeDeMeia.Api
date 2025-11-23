using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Repository;

namespace PeDeMeia.Service
{
    public class ReceitaService
    {
        private readonly ReceitaRepository _receitaRepository;

        public ReceitaService(ReceitaRepository receitaRepository)
        {
            _receitaRepository = receitaRepository;
        }

        public List<ReceitaEntity> BuscarTodos()
        {
            return _receitaRepository.BuscarTodos();
        }

        public ReceitaEntity BuscarPorId(int id)
        {
            return _receitaRepository.BuscarPorId(id);
        }

        public int Cadastrar(ReceitaInputModel inputModel)
        {
            var receita = new ReceitaEntity
            {
                Descricao = inputModel.Descricao,
                Categoria = inputModel.Categoria,
                Valor = inputModel.Valor,
                DataReceita = inputModel.DataReceita,
                PessoaId = inputModel.PessoaId,
                BancoId = inputModel.BancoId
            };

            return _receitaRepository.Cadastrar(receita);
        }

        public bool Atualizar(int id, ReceitaInputModel inputModel)
        {
            var receita = new ReceitaEntity
            {
                Descricao = inputModel.Descricao,
                Categoria = inputModel.Categoria,
                Valor = inputModel.Valor,
                DataReceita = inputModel.DataReceita,
                PessoaId = inputModel.PessoaId,
                BancoId = inputModel.BancoId
            };

            return _receitaRepository.Atualizar(id, receita);
        }

        public bool Deletar(int id)
        {
            return _receitaRepository.Deletar(id);
        }

        public List<ReceitaEntity> BuscarPorPessoa(int pessoaId)
        {
            return _receitaRepository.BuscarPorPessoa(pessoaId);
        }
    }
}