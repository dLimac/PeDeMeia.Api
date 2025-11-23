using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeDeMeia.Domain.Entities
{
    public class ReceitaEntity
    {
        private int _id;
        private string _descricao;
        private string _categoria;
        private decimal _valor;
        private DateTime _dataReceita;
        private int _pessoaId;
        private int _bancoId;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Descricao
        {
            get => _descricao;
            set => _descricao = value;
        }

        public string Categoria
        {
            get => _categoria;
            set => _categoria = value;
        }

        public decimal Valor
        {
            get => _valor;
            set => _valor = value;
        }

        public DateTime DataReceita
        {
            get => _dataReceita;
            set => _dataReceita = value;
        }

        public int PessoaId
        {
            get => _pessoaId;
            set => _pessoaId = value;
        }

        public int BancoId
        {
            get => _bancoId;
            set => _bancoId = value;
        }
    }
}
