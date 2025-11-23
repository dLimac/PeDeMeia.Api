using System.Data.SqlClient;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Data;

namespace PeDeMeia.Infra.Repository
{
    public class ReceitaRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public ReceitaRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public List<ReceitaEntity> BuscarTodos()
        {
            var receitas = new List<ReceitaEntity>();
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "SELECT Id, Descricao, Categoria, Valor, DataReceita, PessoaId, BancoId FROM Receita ORDER BY DataReceita DESC";
            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                receitas.Add(new ReceitaEntity
                {
                    Id = reader.GetInt32(0),
                    Descricao = reader.GetString(1),
                    Categoria = reader.GetString(2),
                    Valor = reader.GetDecimal(3),
                    DataReceita = reader.GetDateTime(4),
                    PessoaId = reader.GetInt32(5),
                    BancoId = reader.GetInt32(6)
                });
            }
            return receitas;
        }

        public ReceitaEntity BuscarPorId(int id)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "SELECT Id, Descricao, Categoria, Valor, DataReceita, PessoaId, BancoId FROM Receita WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new ReceitaEntity
                {
                    Id = reader.GetInt32(0),
                    Descricao = reader.GetString(1),
                    Categoria = reader.GetString(2),
                    Valor = reader.GetDecimal(3),
                    DataReceita = reader.GetDateTime(4),
                    PessoaId = reader.GetInt32(5),
                    BancoId = reader.GetInt32(6)
                };
            }
            return null;
        }

        public int Cadastrar(ReceitaEntity receita)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            using var transaction = connection.BeginTransaction();

            try
            {
                var query = @"INSERT INTO Receita (Descricao, Categoria, Valor, DataReceita, PessoaId, BancoId) 
                             OUTPUT INSERTED.Id
                             VALUES (@Descricao, @Categoria, @Valor, @DataReceita, @PessoaId, @BancoId)";
                using var command = new SqlCommand(query, connection, transaction);
                command.Parameters.AddWithValue("@Descricao", receita.Descricao);
                command.Parameters.AddWithValue("@Categoria", receita.Categoria);
                command.Parameters.AddWithValue("@Valor", receita.Valor);
                command.Parameters.AddWithValue("@DataReceita", receita.DataReceita);
                command.Parameters.AddWithValue("@PessoaId", receita.PessoaId);
                command.Parameters.AddWithValue("@BancoId", receita.BancoId);

                int id = (int)command.ExecuteScalar();

                var updateQuery = "UPDATE Banco SET Saldo = Saldo + @Valor WHERE Id = @BancoId";
                using var updateCommand = new SqlCommand(updateQuery, connection, transaction);
                updateCommand.Parameters.AddWithValue("@Valor", receita.Valor);
                updateCommand.Parameters.AddWithValue("@BancoId", receita.BancoId);
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

        public bool Atualizar(int id, ReceitaEntity receita)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            using var transaction = connection.BeginTransaction();

            try
            {
                var selectQuery = "SELECT Valor, BancoId FROM Receita WHERE Id = @Id";
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

                var restoreQuery = "UPDATE Banco SET Saldo = Saldo - @ValorAnterior WHERE Id = @BancoIdAnterior";
                using var restoreCommand = new SqlCommand(restoreQuery, connection, transaction);
                restoreCommand.Parameters.AddWithValue("@ValorAnterior", valorAnterior);
                restoreCommand.Parameters.AddWithValue("@BancoIdAnterior", bancoIdAnterior);
                restoreCommand.ExecuteNonQuery();

                var updateQuery = @"UPDATE Receita SET Descricao = @Descricao, Categoria = @Categoria, 
                                   Valor = @Valor, DataReceita = @DataReceita, PessoaId = @PessoaId, BancoId = @BancoId 
                                   WHERE Id = @Id";
                using var updateCommand = new SqlCommand(updateQuery, connection, transaction);
                updateCommand.Parameters.AddWithValue("@Id", id);
                updateCommand.Parameters.AddWithValue("@Descricao", receita.Descricao);
                updateCommand.Parameters.AddWithValue("@Categoria", receita.Categoria);
                updateCommand.Parameters.AddWithValue("@Valor", receita.Valor);
                updateCommand.Parameters.AddWithValue("@DataReceita", receita.DataReceita);
                updateCommand.Parameters.AddWithValue("@PessoaId", receita.PessoaId);
                updateCommand.Parameters.AddWithValue("@BancoId", receita.BancoId);
                updateCommand.ExecuteNonQuery();

                var addQuery = "UPDATE Banco SET Saldo = Saldo + @Valor WHERE Id = @BancoId";
                using var addCommand = new SqlCommand(addQuery, connection, transaction);
                addCommand.Parameters.AddWithValue("@Valor", receita.Valor);
                addCommand.Parameters.AddWithValue("@BancoId", receita.BancoId);
                addCommand.ExecuteNonQuery();

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
                var selectQuery = "SELECT Valor, BancoId FROM Receita WHERE Id = @Id";
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

                var deleteQuery = "DELETE FROM Receita WHERE Id = @Id";
                using var deleteCommand = new SqlCommand(deleteQuery, connection, transaction);
                deleteCommand.Parameters.AddWithValue("@Id", id);
                deleteCommand.ExecuteNonQuery();

                var updateQuery = "UPDATE Banco SET Saldo = Saldo - @Valor WHERE Id = @BancoId";
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

        public List<ReceitaEntity> BuscarPorPessoa(int pessoaId)
        {
            var receitas = new List<ReceitaEntity>();
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "SELECT Id, Descricao, Categoria, Valor, DataReceita, PessoaId, BancoId FROM Receita WHERE PessoaId = @PessoaId ORDER BY DataReceita DESC";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PessoaId", pessoaId);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                receitas.Add(new ReceitaEntity
                {
                    Id = reader.GetInt32(0),
                    Descricao = reader.GetString(1),
                    Categoria = reader.GetString(2),
                    Valor = reader.GetDecimal(3),
                    DataReceita = reader.GetDateTime(4),
                    PessoaId = reader.GetInt32(5),
                    BancoId = reader.GetInt32(6)
                });
            }
            return receitas;
        }
    }
}