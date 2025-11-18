using Microsoft.EntityFrameworkCore;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeDeMeia.Infra.Repository
{
    public class PessoaRepository
    {
        public bool Cadastrar(PessoaEntity p)
        {
                try
                {
                    BancoInstance banco;
                    using (banco = new BancoInstance())
                    {
                        return banco.Banco.ExecuteNonQuery(@"insert into Pessoa (Nome, Cpf, ConjugeOuParentesco, Saldo) values (@nome, @cpf @conjuge, @saldo)", "@nome", p.Nome, "@cpf", p.Cpf, "@conjuge", p.Conjuge,"@saldo", p.Saldo);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            
        }


        #region Metodos Auxiliares
        private PessoaEntity ConvertToObject(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0)
                    return new PessoaEntity();
                else
                {
                    return new PessoaEntity(
                        Convert.ToInt32(dt.Rows[0]["Id"]),
                        dt.Rows[0]["Nome"].ToString(),
                        dt.Rows[0]["Cpf"].ToString(),
                        dt.Rows[0]["Conjuge"].ToString(),
                        decimal.Parse((string)dt.Rows[0]["Saldo"]));
                }
            }
            catch (Exception e)
            {
                return new PessoaEntity();
            }
        }
        #endregion
    }
}
