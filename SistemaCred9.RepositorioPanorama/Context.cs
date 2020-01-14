using MySql.Data.MySqlClient;
using System.Data;

namespace SistemaCred9.RepositorioPanorama
{
    public class Context
    {
        private readonly MySqlConnection _myConnection;
        private MySqlTransaction _tran;

        public Context(string conString)
        {
            try
            {
                _myConnection = new MySqlConnection(conString);
            }
            catch
            {
                throw;
            }
        }

        public void ExecuteSqlCommandNoReturn(string command)
        {
            try
            {
                var cmdComando = new MySqlCommand
                {
                    CommandText = command,
                    CommandType = CommandType.Text,
                    Connection = _myConnection,
                    Transaction = _tran,
                    CommandTimeout = int.MaxValue
                };

                cmdComando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public MySqlDataReader ExecuteSqlCommandWithReturn(string commmand)
        {
            try
            {
                MySqlDataReader reader = null;

                var cmdComando = new MySqlCommand
                {
                    CommandText = commmand,
                    CommandType = CommandType.Text,
                    Connection = _myConnection,
                    Transaction = _tran,
                    CommandTimeout = int.MaxValue
                };

                reader = cmdComando.ExecuteReader();

                return reader;
            }
            catch
            {
                throw;
            }
        }

        public MySqlDataReader ExecuteSqlCommandWithReturnNoTransaction(string commmand)
        {
            try
            {
                MySqlDataReader reader = null;

                var cmdComando = new MySqlCommand
                {
                    CommandText = commmand,
                    CommandType = CommandType.Text,
                    Connection = _myConnection,
                    CommandTimeout = int.MaxValue
                };

                reader = cmdComando.ExecuteReader();

                return reader;
            }
            catch
            {
                throw;
            }
        }

        public void OpenTransaction()
        {
            _tran = _myConnection.BeginTransaction();
        }

        public void Commit()
        {
            _tran.Commit();
        }

        public void RollBack()
        {
            _tran.Rollback();
        }

        public void AbrirConexao()
        {
            if (_myConnection.State == ConnectionState.Closed)
            {
                _myConnection.Open();
            }
        }

        public void FecharConexao()
        {
            if (_myConnection.State == ConnectionState.Open)
            {
                _myConnection.Close();
            }
        }

        public void Dispose()
        {
            if (_myConnection.State == ConnectionState.Open)
            {
                _myConnection.Close();
            }
        }
    }
}
