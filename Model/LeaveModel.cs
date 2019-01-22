using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Model
{
    public class LeaveModel
    {
        public int StaffId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string HalfPeriod { get; set; }
        public decimal Days { get; set; }
        public string TypeOfLeave { get; set; }
    }
}
