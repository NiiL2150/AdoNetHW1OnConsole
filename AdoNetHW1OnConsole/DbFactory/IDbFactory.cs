using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetHW1OnConsole.DbFactory
{
    public interface IDbFactory
    {
        IDbConnection CreateConnection(string _config);
        IDbCommand CreateCommand(string _query, IDbConnection _connection);
        IDataReader CreateReader(IDbCommand _command);
    }
}

