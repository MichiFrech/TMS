using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    public class Notification
    {
        public int notification_id { get; set; }
        public string acc_ids { get; set; }
        public string message { get; set; }
        public int proj_id { get; set; }

        public Notification(int n_id, string a_ids, string msg, int p_id)
        {
            this.notification_id = n_id;
            this.acc_ids = a_ids;
            this.message = msg;
            this.proj_id = p_id;
        }

        public bool IsInvitation()
        {
            return message.ToLower().Contains("invitation");
        }

        public override string ToString()
        {
            return message;
        }
    }
}
