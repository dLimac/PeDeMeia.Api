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
    public class DespesaRepository
    {
        public bool Cadastrar(DespesaEntity r)
        {
            if (r.Id == 0)
            {
                try
                {
                    BancoInstance banco;
                    using (banco = new BancoInstance())
                    {
                        return banco.Banco.ExecuteNonQuery(@"insert into Despesa (Descricao, Valor, PessoaId, DataDespesa, ContaBancariaId) values (@descricao, @valor, @pessoaId, @dataDespesa, @contaBancariaId)", "@descricao", r.Descricao, "@valor", r.Valor, "@pessoaId", r.PessoaId, "@dataDespesa", r.DataDespesa, "@contaBancariaId", r.ContaBancariaId);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else if (r.Id != 0)
            {
                return false;
            }
            return false;
        }

        public bool Atualizar(DespesaEntity r)
        {
            try
            {
                BancoInstance banco;
                using (banco = new BancoInstance())
                {
                    return banco.Banco.ExecuteNonQuery(@"insert into Despesa (Descricao, Categoria, Valor, PessoaId, DataDespesa, ContaBancariaId) values (@descricao, @categoria, @valor, @pessoaId, @dataDespesa, @contaBancariaId)", "@descricao", r.Descricao, "@categoria", r.Categoria, "@valor", r.Valor, "@pessoaId", r.PessoaId, "@dataDespesa", r.DataDespesa, "@contaBancariaId", r.ContaBancariaId);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public DespesaEntity ObterPorCategoria(string categoria)
        {
            DataTable dt = new DataTable();

            try
            {
                BancoInstance conexao;
                using (conexao = new BancoInstance())
                {
                    conexao.Banco.ExecuteQuery(@"select * from Despesa where Categoria=@var", out dt, "@var", categoria);
                }
                return ConvertToObject(dt);
            }
            catch (Exception e)
            {
                return new DespesaEntity();
            }
        }

        public DespesaEntity ObterTodos()
        {
            DataTable dt = new DataTable();

            try
            {
                BancoInstance conexao;
                using (conexao = new BancoInstance())
                {
                    conexao.Banco.ExecuteNonQuery(@"select * from Despesa");
                }
                return ConvertToObject(dt);
            }
            catch (Exception e)
            {
                return new DespesaEntity();
            }
        }

        #region Metodos Auxiliares
        private DespesaEntity ConvertToObject(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0)
                    return new DespesaEntity();
                else
                {
                    return new DespesaEntity(
                        Convert.ToInt32(dt.Rows[0]["Id"]),
                        dt.Rows[0]["Descricao"].ToString(),
                        dt.Rows[0]["Categoria"].ToString(),
                        decimal.Parse((string)dt.Rows[0]["Valor"]),
                        Convert.ToInt32(dt.Rows[0]["PessoaId"]),
                        DateTime.Parse((string)dt.Rows[0]["DataDespesa"]),
                        Convert.ToInt32(dt.Rows[0]["ContaBancariaId"]));
                }
            }
            catch (Exception e)
            {
                return new DespesaEntity();
            }
        }
        #endregion
    }
}

