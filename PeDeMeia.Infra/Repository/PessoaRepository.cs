using System.Data.SqlClient;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Data;

namespace PeDeMeia.Infra.Repository
{
    public class PessoaRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public PessoaRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public List<PessoaEntity> BuscarTodos()
        {
            var pessoas = new List<PessoaEntity>();
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "SELECT Id, Nome, Cpf, ConjugeOuParentesco, Saldo FROM Pessoa";
            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                pessoas.Add(new PessoaEntity
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Cpf = reader.GetString(2),
                    ConjugeOuParentesco = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Saldo = reader.GetDecimal(4)
                });
            }
            return pessoas;
        }

        public PessoaEntity BuscarPorId(int id)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "SELECT Id, Nome, Cpf, ConjugeOuParentesco, Saldo FROM Pessoa WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new PessoaEntity
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Cpf = reader.GetString(2),
                    ConjugeOuParentesco = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Saldo = reader.GetDecimal(4)
                };
            }
            return null;
        }

        public int Cadastrar(PessoaEntity pessoa)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            var query = @"INSERT INTO Pessoa (Nome, Cpf, ConjugeOuParentesco, Saldo) 
                         OUTPUT INSERTED.Id
                         VALUES (@Nome, @Cpf, @ConjugeOuParentesco, @Saldo)";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Nome", pessoa.Nome);
            command.Parameters.AddWithValue("@Cpf", pessoa.Cpf);
            command.Parameters.AddWithValue("@ConjugeOuParentesco", (object)pessoa.ConjugeOuParentesco ?? DBNull.Value);
            command.Parameters.AddWithValue("@Saldo", pessoa.Saldo);

            return (int)command.ExecuteScalar();
        }

        public bool Atualizar(int id, PessoaEntity pessoa)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            var query = @"UPDATE Pessoa SET Nome = @Nome, Cpf = @Cpf, 
                         ConjugeOuParentesco = @ConjugeOuParentesco, Saldo = @Saldo 
                         WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Nome", pessoa.Nome);
            command.Parameters.AddWithValue("@Cpf", pessoa.Cpf);
            command.Parameters.AddWithValue("@ConjugeOuParentesco", (object)pessoa.ConjugeOuParentesco ?? DBNull.Value);
            command.Parameters.AddWithValue("@Saldo", pessoa.Saldo);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Deletar(int id)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "DELETE FROM Pessoa WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            return command.ExecuteNonQuery() > 0;
        }
    }
}