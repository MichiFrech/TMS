using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using System.Windows.Media;
using System.IO;

namespace TMS
{
    public class Account
    {
        #region Account properties
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool businessusage { get; set; }
        public string date { get; set; }
        public DateTime lastpay { get; set; }
        public string description { get; set; }
        #endregion

        public Account(string line)
        {
            fromString(line);
        }
        public Account(int id, string firstn, string lastn)
        {
            this.id = id;
            this.firstName = firstn;
            this.lastName = lastn;
        }

        public override string ToString()
        {
            return this.id + ";" + this.firstName + ";" + this.lastName + ";" + this.email + ";" + this.password + ";" + businessusage + ";" + date + ";" + description;
        }

        private void fromString(string infos)
        {
            string[] temp = infos.Split(';');
            id = Convert.ToInt16(temp[0]);
            firstName = temp[1];
            lastName = temp[2];
            email = temp[3];
            password = temp[4];
            businessusage = false;
            if (temp[5] == "TRUE")
                businessusage = true;
            if (temp.Length >= 7) {
                lastpay = DateTime.ParseExact(temp[6], "dd-MM-yyyy", null);
                date = lastpay.ToString();
            }
            else
                date = "-----";
            if (temp.Length >= 8)
                description = temp[7];
            else
                description = "-----";
        }
    }
}