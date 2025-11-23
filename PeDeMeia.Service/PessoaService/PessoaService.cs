using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Repository;

namespace PeDeMeia.Service
{
    public class PessoaService
    {
        private readonly PessoaRepository _pessoaRepository;

        public PessoaService(PessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public List<PessoaEntity> BuscarTodos()
        {
            return _pessoaRepository.BuscarTodos();
        }

        public PessoaEntity BuscarPorId(int id)
        {
            return _pessoaRepository.BuscarPorId(id);
        }

        public int Cadastrar(PessoaInputModel inputModel)
        {
            var pessoa = new PessoaEntity
            {
                Nome = inputModel.Nome,
                Cpf = inputModel.Cpf,
                ConjugeOuParentesco = inputModel.ConjugeOuParentesco,
                Saldo = inputModel.Saldo
            };

            return _pessoaRepository.Cadastrar(pessoa);
        }

        public bool Atualizar(int id, PessoaInputModel inputModel)
        {
            var pessoa = new PessoaEntity
            {
                Nome = inputModel.Nome,
                Cpf = inputModel.Cpf,
                ConjugeOuParentesco = inputModel.ConjugeOuParentesco,
                Saldo = inputModel.Saldo
            };

            return _pessoaRepository.Atualizar(id, pessoa);
        }

        public bool Deletar(int id)
        {
            return _pessoaRepository.Deletar(id);
        }
    }
}
