using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Repository;

namespace PeDeMeia.Service.DespesaService
{
    public class BuscaDespesaService
    {
        public DespesaEntity ObterPorCategoria(string categoria)
            => new DespesaRepository().ObterPorCategoria(categoria);

        public DespesaEntity ObterTodos()
            => new DespesaRepository().ObterTodos();
    }
}
