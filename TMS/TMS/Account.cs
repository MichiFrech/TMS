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
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public Account(string line)
        {
            fromString(line);
        }

        public override string ToString()
        {
            return this.id + ";" + this.firstName + ";" + this.lastName + ";" + this.email + ";" + this.password;
        }

        private void fromString(string infos)
        {
            string[] temp = infos.Split(';');
            id = Convert.ToInt16(temp[0]);
            firstName = temp[1];
            lastName = temp[2];
            email = temp[3];
            password = temp[4];
        }
    }
}