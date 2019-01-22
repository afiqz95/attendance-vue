using AttendanceSystem.Model;
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

        public async Task<bool> InsertNewStaff(string userId, string name)
        {
            var query = "INSERT INTO STAFF(ID,Name) VALUES(@ID,@NAME)";

            using (var command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@ID", userId);
                command.Parameters.AddWithValue("@NAME", name);

                var result = await command.ExecuteNonQueryAsync();
                if (result == 1)
                    return true;
                else
                    return false;
            }
        }


        public async Task<IEnumerable<AttendanceModel>> GetAttendance()
        {
            List<AttendanceModel> models = new List<AttendanceModel>();

            var query = "SELECT STAFF.ID, STAFF.NAME, ATTENDRECORD.ATTENDDATE FROM STAFF " +
                "INNER JOIN ATTENDRECORD ON STAFF.ID = ATTENDRECORD.ID";

            using (var command = new SqlCommand(query, conn))
            {
                var result = await command.ExecuteReaderAsync();
                while (await result.ReadAsync())
                {
                    models.Add(new AttendanceModel
                    {
                        DateTime = result.GetDateTime(2),
                        UserId = Convert.ToString(result.GetInt32(0)),
                        StaffName = result.GetString(1)
                    });
                }
            }
            return models;
        }

        public async Task<IEnumerable<AttendanceModel>> GetAllUsers()
        {
            List<AttendanceModel> models = new List<AttendanceModel>();
            var name = "SELECT * FROM STAFF";

            using (var command = new SqlCommand(name, conn))
            {
                var result = await command.ExecuteReaderAsync();
                while (await result.ReadAsync())
                {
                    models.Add(new AttendanceModel
                    {
                        UserId = Convert.ToString(result.GetInt32(0)),
                        StaffName = result.GetString(1)
                    });
                }
            }

            return models;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var query = "DELETE FROM STAFF WHERE ID = @ID";

            using (var command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@ID", userId);

                var result = await command.ExecuteNonQueryAsync();
                if (result == 1)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> InsertNewLeave(LeaveModel model)
        {
            string sb;

            if (model.From.HasValue && model.From.HasValue)
            {
                sb = "INSERT INTO [dbo].[LeaveRecord]" +
"           ([StaffId]" +
"           ,[From]" +
"           ,[To]" +
"           ,[HalfPeriod]" +
"           ,[Days]" +
"           ,[TypeOfLeave])" +
"     VALUES" +
"           (@StaffId" +
"           ,@From" +
"           ,@To" +
"           ,@HalfPeriod" +
"           ,@Days" +
"           ,@TypeOfLeave)";
            }
            else
            {
                sb = "INSERT INTO [dbo].[LeaveRecord]" +
"           ([StaffId]" +
"           ,[Days])" +
"     VALUES" +
"           (@StaffId" +
"           ,@Days)";
            }

            using (var cmd = new SqlCommand(sb, conn))
            {
                cmd.Parameters.AddWithValue("@StaffId", model.StaffId);
                cmd.Parameters.AddWithValue("@Days", model.Days);
                if (model.From.HasValue && model.To.HasValue)
                {
                    cmd.Parameters.AddWithValue("@From", model.From);
                    cmd.Parameters.AddWithValue("@To", model.To);
                    cmd.Parameters.AddWithValue("@HalfPeriod", model.HalfPeriod);
                    cmd.Parameters.AddWithValue("@TypeOfLeave", model.TypeOfLeave);
                }

                var res = await cmd.ExecuteNonQueryAsync();
                if (res > 0)
                    return true;
                else
                    return false;
            }
        }

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
