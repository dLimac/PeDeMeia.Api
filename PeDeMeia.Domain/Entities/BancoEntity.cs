using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeDeMeia.Domain.Entities
{
    public class BancoEntity
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Saldo { get; private set; }
        public int PessoaId { get; private set; }

        public BancoEntity() { }

        public BancoEntity(int id, string nome, decimal saldo, int pessoaId)
        {
            Id = id;
            Nome = nome;
            Saldo = saldo;
            PessoaId = pessoaId;
        }
    }
}
