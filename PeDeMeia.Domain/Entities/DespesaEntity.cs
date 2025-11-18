using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeDeMeia.Domain.Entities
{
    public class DespesaEntity
    {
        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public string Categoria { get; private set; }
        public decimal Valor { get; private set; }
        public int PessoaId { get; private set; }
        public DateTime DataDespesa { get; private set; }
        public int ContaBancariaId { get; private set; }
        public DespesaEntity() { }

        public DespesaEntity(int id, string descricao, string categoria, decimal valor, int pessoaId, DateTime dataDespesa, int contaBancariaId)
        {
            Id = id;
            Descricao = descricao;
            Categoria = categoria;
            Valor = valor;
            PessoaId = pessoaId;
            DataDespesa = dataDespesa;
            ContaBancariaId = contaBancariaId;
        }
    }
}
