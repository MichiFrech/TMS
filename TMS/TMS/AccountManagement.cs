using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    class AccountManagement
    {
        public static void DeleteAccount(Account acc)
        {
            string element = acc.ToString();
            string[] temp = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Accounts.csv");
            List<string> accounts = new List<string>();
            for(int i = 0; i < temp.Length; i++)
                if(temp[i] != element)
                    accounts.Add(temp[i]);

            File.Delete(Directory.GetCurrentDirectory() + "/Accounts.csv");
            if(temp.Length > 2)
                File.WriteAllLines(Directory.GetCurrentDirectory() + "/Accounts.csv", accounts.ToArray());
        }
    }
}
