using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace TMS.views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        private Account acc;
        private MainWindow mwinstance;
        public AccountView()
        {
            InitializeComponent();
            this.acc = MainWindow.account;
            MainWindow.account = null;
            this.mwinstance = MainWindow.instance;
            lal.Content = acc.firstName + " " + acc.lastName + " " + acc.email + " " + acc.password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccountManagement.DeleteAccount(this.acc);
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            FrontPage fp = new FrontPage();
            fp.Show();
            metroWindow.Close();
        }
    }
}
