using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeDeMeia.Domain.DTOs.OutputModel
{
    public class UsuarioOutputModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Conjuge { get; set; }
        public decimal Saldo { get; set; }
    }
}
