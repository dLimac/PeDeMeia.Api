using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Repository;

namespace PeDeMeia.Service.ReceitaService
{
    public class BuscaReceitaService
    {
        public ReceitaEntity ObterPorCategoria(string categoria)
            => new ReceitaRepository().ObterPorCategoria(categoria);

        public ReceitaEntity ObterTodos()
            => new ReceitaRepository().ObterTodos();
    }
}
