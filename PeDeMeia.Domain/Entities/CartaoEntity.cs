using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeDeMeia.Domain.Entities
{
    public class CartaoEntity
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Limite { get; private set; }
        public int PessoaId { get; private set; }
        public int DiaVencimentoFatura { get; private set; }

        public CartaoEntity() { }

        public CartaoEntity(int id, string nome, decimal limite, int pessoaId, int diaVencimentoFatura)
        {
            Id = id;
            Nome = nome;
            Limite = limite;
            PessoaId = pessoaId;
            DiaVencimentoFatura = diaVencimentoFatura;
        }

    }
}
