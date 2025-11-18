using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeDeMeia.Domain.Entities
{
    public class PessoaEntity
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Conjuge { get; private set; }
        public decimal Saldo { get; private set; }
        /* public List<BancoEntity> Bancos { get; private set; } = new();
        public List<CartaoEntity> Cartoes { get; private set; } = new();
        public List<ReceitaEntity> Receitas { get; private set; } = new();
        public List<DespesaEntity> Despesas { get; private set; } = new(); */
        
        public PessoaEntity()
        {
            
        }

        public PessoaEntity(int id, string nome, string cpf, string conjuge, decimal saldo)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Conjuge = conjuge;
            Saldo = saldo;
        }
    }
}
