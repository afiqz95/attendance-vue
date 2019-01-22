using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceSystem.Model;
using AttendanceSystem.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AttendanceSystem.Controllers
{
    [Route("api/leave")]
    public class LeaveApiController : Controller
    {
        [Route("")]
        public async Task<IActionResult> InsertLeave([FromBody]LeaveModel model)
        {
            using (var factory = new SqlConnectionFactory())
            {
                return Ok(await factory.InsertNewLeave(model));
            }
        }
    }
}
