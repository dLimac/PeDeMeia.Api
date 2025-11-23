using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeDeMeia.Domain.Entities
{
    public class BancoEntity
    {
        private int _id;
        private string _nome;
        private decimal _saldo;
        private int _pessoaId;

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

        public decimal Saldo
        {
            get => _saldo;
            set => _saldo = value;
        }

        public int PessoaId
        {
            get => _pessoaId;
            set => _pessoaId = value;
        }
    }
}
