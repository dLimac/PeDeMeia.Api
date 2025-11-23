using System.Data.SqlClient;

namespace PeDeMeia.Infra.Data
{
    public class DatabaseInitializer
    {
        private readonly DatabaseConnection _databaseConnection;

        public DatabaseInitializer(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void InitializeDatabase()
        {
            using var connection = _databaseConnection.GetOpenConnection();

            CreateTablePessoa(connection);
            CreateTableBanco(connection);
            CreateTableCartao(connection);
            CreateTableReceita(connection);
            CreateTableDespesa(connection);
        }

        private void CreateTablePessoa(SqlConnection connection)
        {
            var query = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Pessoa')
                BEGIN
                    CREATE TABLE Pessoa (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Nome NVARCHAR(100) NOT NULL,
                        Cpf NVARCHAR(14) NOT NULL,
                        ConjugeOuParentesco NVARCHAR(100),
                        Saldo DECIMAL(18,2) DEFAULT 0
                    )
                END";

            using var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        private void CreateTableBanco(SqlConnection connection)
        {
            var query = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Banco')
                BEGIN
                    CREATE TABLE Banco (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Nome NVARCHAR(100) NOT NULL,
                        Saldo DECIMAL(18,2) DEFAULT 0,
                        PessoaId INT NOT NULL,
                        FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id) ON DELETE CASCADE
                    )
                END";

            using var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        private void CreateTableCartao(SqlConnection connection)
        {
            var query = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Cartao')
                BEGIN
                    CREATE TABLE Cartao (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Limite DECIMAL(18,2) NOT NULL,
                        Nome NVARCHAR(100) NOT NULL,
                        DataVencimentoFatura DATETIME NOT NULL,
                        ValorFatura DECIMAL(18,2) DEFAULT 0,
                        PessoaId INT NOT NULL,
                        FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id) ON DELETE CASCADE
                    )
                END";

            using var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        private void CreateTableReceita(SqlConnection connection)
        {
            var query = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Receita')
                BEGIN
                    CREATE TABLE Receita (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Descricao NVARCHAR(200) NOT NULL,
                        Categoria NVARCHAR(50) NOT NULL,
                        Valor DECIMAL(18,2) NOT NULL,
                        DataReceita DATETIME NOT NULL,
                        PessoaId INT NOT NULL,
                        BancoId INT NOT NULL,
                        FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id),
                        FOREIGN KEY (BancoId) REFERENCES Banco(Id)
                    )
                END";

            using var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        private void CreateTableDespesa(SqlConnection connection)
        {
            var query = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Despesa')
                BEGIN
                    CREATE TABLE Despesa (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Descricao NVARCHAR(200) NOT NULL,
                        Categoria NVARCHAR(50) NOT NULL,
                        Valor DECIMAL(18,2) NOT NULL,
                        DataDespesa DATETIME NOT NULL,
                        PessoaId INT NOT NULL,
                        BancoId INT NOT NULL,
                        FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id),
                        FOREIGN KEY (BancoId) REFERENCES Banco(Id)
                    )
                END";

            using var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }
    }
}