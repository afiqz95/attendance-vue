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

namespace AttendanceSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/FileApi")]
    public class FileApiController : Controller
    {
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var lines = await file.ReadAsStringAsync();

            lines.ToList().ForEach(async line =>
            {
                var splitted = line.Split('\t');

                var temp = new AttendanceModel()
                {
                    UserId = splitted[0].Trim(),
                    DateTime = DateTime.Parse(splitted[1])
                };

                using (SqlConnection connection = new SqlConnection())
                {
                    var sql = $"INSERT INTO heretablename VALUE('wahtever','wtbeerer')";
                    using (SqlCommand cmd = new SqlCommand(sql))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            });

            return Ok(new { Successful = true, Count = lines.Count() });
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