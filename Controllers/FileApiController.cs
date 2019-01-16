using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AttendanceSystem.Services;

namespace AttendanceSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/FileApi")]
    public class FileApiController : Controller
    {
        [Route("uploadfile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var lines = await file.ReadAsStringAsync();

            List<AttendanceModel> Users = new List<AttendanceModel>();
            List<AttendanceModel> Attendances = new List<AttendanceModel>();
            lines.ToList().ForEach(async line =>
            {
                var splitted = line.Split('\t');

                var tempDate = new AttendanceModel()
                {
                    UserId = splitted[0].Trim(),
                    DateTime = DateTime.Parse(splitted[1])
                };

                Attendances.Add(tempDate);
                using (var sql = new SqlConnectionFactory())
                {
                    await sql.InsertNewAttendanceRecord(tempDate.UserId, tempDate.DateTime);
                }
            });

            using (var sql = new SqlConnectionFactory())
            {
                Users = (await sql.GetAllUsers()).ToList();
            }

            var data = Attendances.Select(x =>
            {
                return new AttendanceModel
                {
                    DateTime = x.DateTime,
                    StaffName = Users.Any(z => z.UserId == x.UserId) == false ? "No Data" : Users.Where(y => y.UserId == x.UserId).Select(z => z.StaffName).First(),
                    UserId = x.UserId
                };
            });

            return Ok(new { Successful = true, Count = lines.Count(), data = data.OrderByDescending(x => x.UserId).ToArray() });
        }

        [Route("getAttendance")]
        public async Task<IActionResult> GetAttendances()
        {
            using (var sql = new SqlConnectionFactory())
            {
                return Ok(await sql.GetAttendance());
            }
        }
        [Route("getMockUpAttendance")]
        public IActionResult GetMockupAttendances()
        {
            var lists = new List<AttendanceModel>();
            lists.Add(new AttendanceModel
            {
                UserId = "123",
                DateTime = DateTime.Now,
                StaffName = "Whatever"
            });
            lists.Add(new AttendanceModel
            {
                UserId = "123",
                DateTime = DateTime.Now,
                StaffName = "Whatever"
            });
            lists.Add(new AttendanceModel
            {
                UserId = "123",
                DateTime = DateTime.Now,
                StaffName = "Whatever"
            });
            lists.Add(new AttendanceModel
            {
                UserId = "123",
                DateTime = DateTime.Now,
                StaffName = "Whatever"
            });
            return Ok(lists);
        }

        [Route("newUser")]
        public async Task<IActionResult> NewUser([FromBody]NewUserModel model)
        {
            using (var sql = new SqlConnectionFactory())
            {
                return Ok(await sql.InsertNewStaff(model.Id, model.Name));
            }
        }

        [Route("getStaff")]
        public async Task<IActionResult> getStaff()
        {
            using (var sql = new SqlConnectionFactory())
            {
                return Ok(await sql.GetAllUsers());
            }
        }

        [Route("deleteStaff")]
        public async Task<IActionResult> deleteStaff([FromBody]IEnumerable<NewUserModel> model)
        {
            foreach (var user in model)
            {
                using (var sql = new SqlConnectionFactory())
                {
                    await sql.DeleteUser(user.Id);
                }
            }
            return Ok();
        }
    }


    public static class IFormHelper
    {
        public static async Task<IEnumerable<string>> ReadAsStringAsync(this IFormFile file)
        {
            List<string> result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.Add(await reader.ReadLineAsync());
            }
            return result;
        }
    }
}