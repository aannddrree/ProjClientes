using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using System.IO;

namespace DB
{
    public class ConnectionSQLite
    {

        //Declarar um atributo do tipo SQLiteConnection
        private static SQLiteConnection sqliteConnection;

        //Retorna a conexão com o banco de dados (SQLite)
        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection(@"Data Source=c:\tmp\dbcliente.sqlite");
            sqliteConnection.Open();
            return sqliteConnection;
        }

        //Criação do arquito SQLite
        public static void createDataBaseSQLite(string dataBaseName)
        {
            try
            {
                if (!File.Exists(@"c:\tmp\" + dataBaseName))
                    SQLiteConnection.CreateFile(@"c:\tmp\"+ dataBaseName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Criação da Tabela no SQLite
        public static void CreateTableSQLite(string tableName)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS "+ tableName + " ( id int, nome varchar(50), telefone varchar(20))";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Inclusão das informações na tabela de Cliente (SQLite)
        public static void Add(Cliente cliente)
        {
            using ( var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Clientes (id, nome, telefone) values (@Id, @Nome, @Telefone)";
                cmd.Parameters.AddWithValue("@Id", cliente.Id);
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                cmd.ExecuteNonQuery();
            }
        }

        //Consultar todos os dados na tabela de cliente (SQLite)
        public static DataTable GetAll()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            using (var cmd = DbConnection().CreateCommand())
            {
                cmd.CommandText = "select id, nome, telefone from clientes";
                da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                da.Fill(dt);
                return dt;
            }
        }
    }
}
