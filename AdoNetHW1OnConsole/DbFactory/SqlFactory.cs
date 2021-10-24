using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetHW1OnConsole.DbFactory
{
    public class SqlFactory : IDbFactory
    {
        public IDbConnection CreateConnection(string _config)
        {
            return new SqlConnection(_config);
        }

        public IDbCommand CreateCommand(string _query, IDbConnection _connection)
        {
            return new SqlCommand(_query, _connection as SqlConnection);
        }

        public IDataReader CreateReader(IDbCommand _command)
        {
            return (_command as SqlCommand).ExecuteReader();
        }
    }
}
