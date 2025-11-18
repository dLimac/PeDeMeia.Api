using Microsoft.EntityFrameworkCore;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Data;
using System.Data;

namespace PeDeMeia.Infra.Repository
{
    public class ReceitaRepository
    {
        public bool Cadastrar(ReceitaEntity r)
        {
            if (r.Id == 0)
            {
                try
                {
                    BancoInstance banco;
                    using (banco = new BancoInstance())
                    {
                        return banco.Banco.ExecuteNonQuery(@"insert into Receita (Descricao, Valor, PessoaId, DataReceita, ContaBancariaId) values (@descricao, @valor, @pessoaId, @dataReceita, @contaBancariaId)", "@descricao", r.Descricao, "@valor", r.Valor, "@pessoaId", r.PessoaId, "@dataReceita", r.DataReceita, "@contaBancariaId", r.ContaBancariaId);
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

        public bool Atualizar(ReceitaEntity r)
        {
            try
            {
                BancoInstance banco;
                using (banco = new BancoInstance())
                {
                    return banco.Banco.ExecuteNonQuery(@"insert into Receita (Descricao, Categoria, Valor, PessoaId, DataReceita, ContaBancariaId) values (@descricao, @categoria, @valor, @pessoaId, @dataReceita, @contaBancariaId)", "@descricao", r.Descricao, "@categoria", r.Categoria, "@valor", r.Valor, "@pessoaId", r.PessoaId, "@dataReceita", r.DataReceita, "@contaBancariaId", r.ContaBancariaId);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public ReceitaEntity ObterPorCategoria(string categoria)
        {
            DataTable dt = new DataTable();

            try
            {
                BancoInstance conexao;
                using (conexao = new BancoInstance())
                {
                    conexao.Banco.ExecuteQuery(@"select * from Receita where Categoria=@var", out dt, "@var", categoria);
                }
                return ConvertToObject(dt);
            }
            catch (Exception e)
            {
                return new ReceitaEntity();
            }
        }

        public ReceitaEntity ObterTodos()
        {
            DataTable dt = new DataTable();

            try
            {
                BancoInstance conexao;
                using (conexao = new BancoInstance())
                {
                    conexao.Banco.ExecuteNonQuery(@"select * from Receita");
                }
                return ConvertToObject(dt);
            }
            catch (Exception e)
            {
                return new ReceitaEntity();
            }
        }

        #region Metodos Auxiliares
        private ReceitaEntity ConvertToObject(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0)
                    return new ReceitaEntity();
                else
                {
                    return new ReceitaEntity(
                        Convert.ToInt32(dt.Rows[0]["Id"]),
                        dt.Rows[0]["Descricao"].ToString(),
                        dt.Rows[0]["Categoria"].ToString(),
                        decimal.Parse((string)dt.Rows[0]["Valor"]),
                        Convert.ToInt32(dt.Rows[0]["PessoaId"]),
                        DateTime.Parse((string)dt.Rows[0]["DataReceita"]),
                        Convert.ToInt32(dt.Rows[0]["ContaBancariaId"]));
                }
            }
            catch (Exception e)
            {
                return new ReceitaEntity();
            }
        }
        #endregion
    }
}
