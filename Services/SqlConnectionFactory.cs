using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Services
{
    public class SqlConnectionFactory : IDisposable
    {
        private string connectionString;
        private SqlConnection conn;

        public SqlConnectionFactory()
        {
            connectionString = ConfigHelper.GetSqlConnString();
            this.conn = new SqlConnection(connectionString);
            this.conn.Open();
        }

        public async Task<bool> InsertNewAttendanceRecord(string userId, DateTime dateTime)
        {
            var query = "INSERT INTO ATTENDRECORD(ID,ATTENDDATE) VALUES(@ID,@DateTime)";

            using (var command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@ID", userId);
                command.Parameters.AddWithValue("@DateTime", dateTime);

                var result = await command.ExecuteNonQueryAsync();
                if (result == 1)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> InsertNewUser(string userId, string name)
        {
            var query = "~~~~~";

            using (var command = new SqlCommand(query, conn))
            {
                var result = await command.ExecuteNonQueryAsync();
                if (result == 1)
                    return true;
                else
                    return false;
            }
        }

        //GET SQL COMMAND ["Get Attendance","Get Users"]

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.conn.Close();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
