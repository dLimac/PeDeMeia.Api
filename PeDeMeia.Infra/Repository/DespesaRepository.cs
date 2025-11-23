using System.Data.SqlClient;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Data;

namespace PeDeMeia.Infra.Repository
{
    public class DespesaRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public DespesaRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public List<DespesaEntity> BuscarTodos()
        {
            var despesas = new List<DespesaEntity>();
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "SELECT Id, Descricao, Categoria, Valor, DataDespesa, PessoaId, BancoId FROM Despesa ORDER BY DataDespesa DESC";
            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                despesas.Add(new DespesaEntity
                {
                    Id = reader.GetInt32(0),
                    Descricao = reader.GetString(1),
                    Categoria = reader.GetString(2),
                    Valor = reader.GetDecimal(3),
                    DataDespesa = reader.GetDateTime(4),
                    PessoaId = reader.GetInt32(5),
                    BancoId = reader.GetInt32(6)
                });
            }
            return despesas;
        }

        public DespesaEntity BuscarPorId(int id)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "SELECT Id, Descricao, Categoria, Valor, DataDespesa, PessoaId, BancoId FROM Despesa WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new DespesaEntity
                {
                    Id = reader.GetInt32(0),
                    Descricao = reader.GetString(1),
                    Categoria = reader.GetString(2),
                    Valor = reader.GetDecimal(3),
                    DataDespesa = reader.GetDateTime(4),
                    PessoaId = reader.GetInt32(5),
                    BancoId = reader.GetInt32(6)
                };
            }
            return null;
        }

        public int Cadastrar(DespesaEntity despesa)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            using var transaction = connection.BeginTransaction();

            try
            {
                var query = @"INSERT INTO Despesa (Descricao, Categoria, Valor, DataDespesa, PessoaId, BancoId) 
                             OUTPUT INSERTED.Id
                             VALUES (@Descricao, @Categoria, @Valor, @DataDespesa, @PessoaId, @BancoId)";
                using var command = new SqlCommand(query, connection, transaction);
                command.Parameters.AddWithValue("@Descricao", despesa.Descricao);
                command.Parameters.AddWithValue("@Categoria", despesa.Categoria);
                command.Parameters.AddWithValue("@Valor", despesa.Valor);
                command.Parameters.AddWithValue("@DataDespesa", despesa.DataDespesa);
                command.Parameters.AddWithValue("@PessoaId", despesa.PessoaId);
                command.Parameters.AddWithValue("@BancoId", despesa.BancoId);

                int id = (int)command.ExecuteScalar();

                var updateQuery = "UPDATE Banco SET Saldo = Saldo - @Valor WHERE Id = @BancoId";
                using var updateCommand = new SqlCommand(updateQuery, connection, transaction);
                updateCommand.Parameters.AddWithValue("@Valor", despesa.Valor);
                updateCommand.Parameters.AddWithValue("@BancoId", despesa.BancoId);
                updateCommand.ExecuteNonQuery();

                transaction.Commit();
                return id;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public bool Atualizar(int id, DespesaEntity despesa)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            using var transaction = connection.BeginTransaction();

            try
            {
                var selectQuery = "SELECT Valor, BancoId FROM Despesa WHERE Id = @Id";
                using var selectCommand = new SqlCommand(selectQuery, connection, transaction);
                selectCommand.Parameters.AddWithValue("@Id", id);
                using var reader = selectCommand.ExecuteReader();

                if (!reader.Read())
                {
                    transaction.Rollback();
                    return false;
                }

                decimal valorAnterior = reader.GetDecimal(0);
                int bancoIdAnterior = reader.GetInt32(1);
                reader.Close();

                var restoreQuery = "UPDATE Banco SET Saldo = Saldo + @ValorAnterior WHERE Id = @BancoIdAnterior";
                using var restoreCommand = new SqlCommand(restoreQuery, connection, transaction);
                restoreCommand.Parameters.AddWithValue("@ValorAnterior", valorAnterior);
                restoreCommand.Parameters.AddWithValue("@BancoIdAnterior", bancoIdAnterior);
                restoreCommand.ExecuteNonQuery();

                var updateQuery = @"UPDATE Despesa SET Descricao = @Descricao, Categoria = @Categoria, 
                                   Valor = @Valor, DataDespesa = @DataDespesa, PessoaId = @PessoaId, BancoId = @BancoId 
                                   WHERE Id = @Id";
                using var updateCommand = new SqlCommand(updateQuery, connection, transaction);
                updateCommand.Parameters.AddWithValue("@Id", id);
                updateCommand.Parameters.AddWithValue("@Descricao", despesa.Descricao);
                updateCommand.Parameters.AddWithValue("@Categoria", despesa.Categoria);
                updateCommand.Parameters.AddWithValue("@Valor", despesa.Valor);
                updateCommand.Parameters.AddWithValue("@DataDespesa", despesa.DataDespesa);
                updateCommand.Parameters.AddWithValue("@PessoaId", despesa.PessoaId);
                updateCommand.Parameters.AddWithValue("@BancoId", despesa.BancoId);
                updateCommand.ExecuteNonQuery();

                var subtractQuery = "UPDATE Banco SET Saldo = Saldo - @Valor WHERE Id = @BancoId";
                using var subtractCommand = new SqlCommand(subtractQuery, connection, transaction);
                subtractCommand.Parameters.AddWithValue("@Valor", despesa.Valor);
                subtractCommand.Parameters.AddWithValue("@BancoId", despesa.BancoId);
                subtractCommand.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public bool Deletar(int id)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            using var transaction = connection.BeginTransaction();

            try
            {
                var selectQuery = "SELECT Valor, BancoId FROM Despesa WHERE Id = @Id";
                using var selectCommand = new SqlCommand(selectQuery, connection, transaction);
                selectCommand.Parameters.AddWithValue("@Id", id);
                using var reader = selectCommand.ExecuteReader();

                if (!reader.Read())
                {
                    transaction.Rollback();
                    return false;
                }

                decimal valor = reader.GetDecimal(0);
                int bancoId = reader.GetInt32(1);
                reader.Close();

                var deleteQuery = "DELETE FROM Despesa WHERE Id = @Id";
                using var deleteCommand = new SqlCommand(deleteQuery, connection, transaction);
                deleteCommand.Parameters.AddWithValue("@Id", id);
                deleteCommand.ExecuteNonQuery();

                var updateQuery = "UPDATE Banco SET Saldo = Saldo + @Valor WHERE Id = @BancoId";
                using var updateCommand = new SqlCommand(updateQuery, connection, transaction);
                updateCommand.Parameters.AddWithValue("@Valor", valor);
                updateCommand.Parameters.AddWithValue("@BancoId", bancoId);
                updateCommand.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<DespesaEntity> BuscarPorPessoa(int pessoaId)
        {
            var despesas = new List<DespesaEntity>();
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "SELECT Id, Descricao, Categoria, Valor, DataDespesa, PessoaId, BancoId FROM Despesa WHERE PessoaId = @PessoaId ORDER BY DataDespesa DESC";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PessoaId", pessoaId);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                despesas.Add(new DespesaEntity
                {
                    Id = reader.GetInt32(0),
                    Descricao = reader.GetString(1),
                    Categoria = reader.GetString(2),
                    Valor = reader.GetDecimal(3),
                    DataDespesa = reader.GetDateTime(4),
                    PessoaId = reader.GetInt32(5),
                    BancoId = reader.GetInt32(6)
                });
            }
            return despesas;
        }
    }
}