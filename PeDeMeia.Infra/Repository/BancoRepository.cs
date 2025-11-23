using System.Data.SqlClient;
using PeDeMeia.Domain.Entities;
using PeDeMeia.Infra.Data;

namespace PeDeMeia.Infra.Repository
{
    public class BancoRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public BancoRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public List<BancoEntity> BuscarTodos()
        {
            var bancos = new List<BancoEntity>();
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "SELECT Id, Nome, Saldo, PessoaId FROM Banco";
            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                bancos.Add(new BancoEntity
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Saldo = reader.GetDecimal(2),
                    PessoaId = reader.GetInt32(3)
                });
            }
            return bancos;
        }

        public BancoEntity BuscarPorId(int id)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "SELECT Id, Nome, Saldo, PessoaId FROM Banco WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new BancoEntity
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Saldo = reader.GetDecimal(2),
                    PessoaId = reader.GetInt32(3)
                };
            }
            return null;
        }

        public int Cadastrar(BancoEntity banco)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            var query = @"INSERT INTO Banco (Nome, Saldo, PessoaId) 
                         OUTPUT INSERTED.Id
                         VALUES (@Nome, @Saldo, @PessoaId)";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Nome", banco.Nome);
            command.Parameters.AddWithValue("@Saldo", banco.Saldo);
            command.Parameters.AddWithValue("@PessoaId", banco.PessoaId);

            return (int)command.ExecuteScalar();
        }

        public bool Atualizar(int id, BancoEntity banco)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            var query = @"UPDATE Banco SET Nome = @Nome, Saldo = @Saldo, PessoaId = @PessoaId 
                         WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Nome", banco.Nome);
            command.Parameters.AddWithValue("@Saldo", banco.Saldo);
            command.Parameters.AddWithValue("@PessoaId", banco.PessoaId);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Deletar(int id)
        {
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "DELETE FROM Banco WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public List<BancoEntity> BuscarPorPessoa(int pessoaId)
        {
            var bancos = new List<BancoEntity>();
            using var connection = _databaseConnection.GetOpenConnection();
            var query = "SELECT Id, Nome, Saldo, PessoaId FROM Banco WHERE PessoaId = @PessoaId";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PessoaId", pessoaId);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                bancos.Add(new BancoEntity
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Saldo = reader.GetDecimal(2),
                    PessoaId = reader.GetInt32(3)
                });
            }
            return bancos;
        }
    }
}