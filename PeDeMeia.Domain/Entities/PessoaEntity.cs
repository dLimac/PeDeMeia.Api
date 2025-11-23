using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeDeMeia.Domain.Entities
{
    public class PessoaEntity
    {
        private int _id;
        private string _nome;
        private string _cpf;
        private string _conjugeOuParentesco;
        private decimal _saldo;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Nome
        {
            get => _nome;
            set => _nome = value;
        }

        public string Cpf
        {
            get => _cpf;
            set => _cpf = value;
        }

        public string ConjugeOuParentesco
        {
            get => _conjugeOuParentesco;
            set => _conjugeOuParentesco = value;
        }

        public decimal Saldo
        {
            get => _saldo;
            set => _saldo = value;
        }
    }
}
