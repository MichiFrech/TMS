using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    public class CalendarEntry
    {
        public string task_name { get; set; }
        public DateTime task_date { get; set; }

        public CalendarEntry(string name, string date)
        {
            this.task_name = name;
            this.task_date = DateTime.Parse(date);
        }
    }
}
