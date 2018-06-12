using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    public class Task
    {
        public int Task_id { get; set; }
        public int Proj_id { get; set; }
        public string Name { get; set; }
        private DateTime dl { get; set; }
        public string Assignees { get; set; }
        public string Desc { get; set; }
        public bool Done { get; set; }
        public string Deadline { get { return dl.ToString("dd-MM-yyyy"); } }
        public string AssigneesWithoutName { get; set; }

        public Task(int task_id, int proj_id, string name, DateTime deadline, string assignees, string desc, bool done)
        {
            this.Task_id = task_id;
            this.Proj_id = proj_id;
            this.Name = name;
            this.dl = deadline;
            this.Assignees = assignees;
            this.AssigneesWithoutName = assignees;
            this.Desc = desc;
            this.Done = done;
        }

        public override string ToString()
        {
            return "TODO";
        }
    }
}
