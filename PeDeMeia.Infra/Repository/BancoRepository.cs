using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Data;
using System.Data;

namespace PeDeMeia.Infra.Repository
{
    public class BancoRepository
    {
        public bool Cadastrar(BancoEntity b)
        {
           
            try
            {
                using (var banco = new BancoInstance())
                {
                    return banco.Banco.ExecuteNonQuery(
                        @"INSERT INTO Banco (Nome, Saldo, PessoaId) 
                  VALUES (@nome, @saldo, @pessoaId)",
                        "@nome", b.Nome,
                        "@saldo", b.Saldo,
                        "@pessoaId", b.PessoaId
                    );
                }
            }
            catch (Exception ex)
            {
                // Nunca deixe vazio. Logue ou relance.
                Console.WriteLine("Erro ao cadastrar banco: " + ex.Message);
                throw; // ou retorne false, dependendo da lógica.
            }
        }


        #region Metodos Auxiliares
        private BancoEntity ConvertToObject(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0)
                    return new BancoEntity();
                else
                {
                    return new BancoEntity(
                        Convert.ToInt32(dt.Rows[0]["Id"]),
                        dt.Rows[0]["Nome"].ToString(),
                        decimal.Parse((string)dt.Rows[0]["Saldo"]),
                        Convert.ToInt32(dt.Rows[0]["PessoaId"]));
                }
            }
            catch (Exception e)
            {
                return new BancoEntity();
            }
        }
        #endregion
    }
}
