using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    public class SqlConnectionFactory
    {
        private string connectionString;
        private SqlConnection cnn;

        public SqlConnectionFactory()
        {
            connectionString = ConfigHelper.GetSqlConnString();
            cnn = new SqlConnection(connectionString);
            cnn.Open();
        }
    }
}
