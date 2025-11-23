using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeDeMeia.Domain.DTOs.InputModel
{
    public class ReceitaInputModel
    {
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataReceita { get; set; }
        public int PessoaId { get; set; }
        public int BancoId { get; set; }
    }
}
