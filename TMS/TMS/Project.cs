using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    public class Project
    {
        public int proj_id { get; set; }
        public string proj_name { get; set; }
        public DateTime proj_duedate { get; set; }
        public List<string> proj_assignees { get; set; }
        public int proj_owner { get; set; }
        private string assigneesTemp;

        public Project(int id, string name, string date, string assignees, int owner)
        {
            this.proj_id = id;
            this.proj_name = name;
            this.proj_duedate = DateTime.Parse(date);
            this.proj_assignees = assignees.Split(',').ToList();
            this.assigneesTemp = assignees;
            this.proj_owner = owner;
        }

        public string AssigneesToString()
        {
            return assigneesTemp;
        }
    }
}
