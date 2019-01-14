using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Model
{
    public class AttendanceModel
    {
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public string StaffName { get; set; }
    }
}
