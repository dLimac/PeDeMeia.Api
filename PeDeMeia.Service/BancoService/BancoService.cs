using PeDeMeia.Domain.DTOs.InputModel;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Repository;

namespace PeDeMeia.Service
{
    public class BancoService
    {
        private readonly BancoRepository _bancoRepository;

        public BancoService(BancoRepository bancoRepository)
        {
            _bancoRepository = bancoRepository;
        }

        public List<BancoEntity> BuscarTodos()
        {
            return _bancoRepository.BuscarTodos();
        }

        public BancoEntity BuscarPorId(int id)
        {
            return _bancoRepository.BuscarPorId(id);
        }

        public int Cadastrar(BancoInputModel inputModel)
        {
            var banco = new BancoEntity
            {
                Nome = inputModel.Nome,
                Saldo = inputModel.Saldo,
                PessoaId = inputModel.PessoaId
            };

            return _bancoRepository.Cadastrar(banco);
        }

        public bool Atualizar(int id, BancoInputModel inputModel)
        {
            var banco = new BancoEntity
            {
                Nome = inputModel.Nome,
                Saldo = inputModel.Saldo,
                PessoaId = inputModel.PessoaId
            };

            return _bancoRepository.Atualizar(id, banco);
        }

        public bool Deletar(int id)
        {
            return _bancoRepository.Deletar(id);
        }

        public List<BancoEntity> BuscarPorPessoa(int pessoaId)
        {
            return _bancoRepository.BuscarPorPessoa(pessoaId);
        }
    }
}