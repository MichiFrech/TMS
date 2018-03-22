using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    public class Task
    {
        private bool _isMarked;
        private string _name;
        private DateTime _deadLine;
        private string _assignees;

        public bool IsMarked
        {
            get
            {
                return _isMarked;
            }
            set
            {
                _isMarked = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public DateTime Deadline
        {
            get
            {
                return _deadLine;
            }
            set
            {
                _deadLine = value;
            }
        }
        public string Assignees
        {
            get
            {
                return _assignees;
            }
            set
            {
                _assignees = value;
            }
        }
    }
}
