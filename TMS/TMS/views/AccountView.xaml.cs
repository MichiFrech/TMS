using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MahApps.Metro;
using System.ComponentModel;
using System.Data.SqlClient;

namespace TMS.views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {

        #region Database
        private string connectionString = "workstation id=tmsdatabase.mssql.somee.com;packet size=4096;" +
                "user id=mfrech_SQLLogin_1;pwd=x9v9kqzi71;data source=tmsdatabase.mssql.somee.com;" +
                "persist security info=False;initial catalog=tmsdatabase";
        private SqlConnection cnn;
        BackgroundWorker worker_saveAccountSettings;
        BackgroundWorker worker_upgradeAccount;
        bool changed = false;
        #endregion

        public AccountView()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Theme == "BaseLight")
                this.currentTheme.IsChecked = false;
            else
                this.currentTheme.IsChecked = true;

            cnn = new SqlConnection(connectionString);
            worker_saveAccountSettings = new BackgroundWorker();
            worker_saveAccountSettings.DoWork += Worker_saveAccountSettings_DoWork;
            worker_saveAccountSettings.RunWorkerCompleted += Worker_saveAccountSettings_RunWorkerCompleted;
            worker_upgradeAccount = new BackgroundWorker();
            worker_upgradeAccount.DoWork += Worker_upgradeAccount_DoWork;
            worker_upgradeAccount.RunWorkerCompleted += Worker_upgradeAccount_RunWorkerCompleted;
        }

        private void Worker_upgradeAccount_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (changed) {
                btn_upgrade.Visibility = Visibility.Hidden;
                lbl_abu.Visibility = Visibility.Visible;
                lbl_lastpayment.Content = DateTime.Now.ToString("dd-MM-yyyy");
                lbl_license.Content = (string)FindResource("bu_account");
            }
            changed = false;
        }

        private void Worker_upgradeAccount_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();
                command.CommandText = "update accounts set businessusage = 'TRUE', lastpayment = CONVERT (date, GETDATE()) where acc_id = " + MainWindow.account.id + ";";

                cnn.Open();
                command.ExecuteNonQuery();
                changed = true;

                cnn.Close();
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(new Action(() => {
                    btn_upgrade.IsEnabled = true;
                }));
            }
        }

        private void Worker_saveAccountSettings_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btn_save.IsEnabled = true;
            tb_confpassword.Password = "";
        }

        private void Worker_saveAccountSettings_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();
                command.CommandText = e.Argument.ToString();

                cnn.Open();
                command.ExecuteNonQuery();

                cnn.Close();
            } catch (Exception ex) {

            }
        }

        private void currentTheme_Click(object sender, RoutedEventArgs e)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, ThemeManager.GetAppTheme((bool)currentTheme.IsChecked ? "BaseDark" : "BaseLight"));

            Properties.Settings.Default.Theme = (bool)currentTheme.IsChecked ? "BaseDark" : "BaseLight";
            Properties.Settings.Default.Save();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Account temp = MainWindow.account;
            tb_firstname.Text = temp.firstName;
            tb_lastname.Text = temp.lastName;
            tb_mail.Text = temp.email;
            lbl_mail.Content = temp.email;
            tb_password.Password = Certify.Decrypt(temp.password);
            TextRange textRange = new TextRange(tb_desc.Document.ContentStart, tb_desc.Document.ContentEnd);
            if (temp.description != null)
                textRange.Text = temp.description;
            if(temp.businessusage)
            {
                btn_upgrade.Visibility = Visibility.Hidden;
                lbl_abu.Visibility = Visibility.Visible;
                lbl_license.Content = (string)FindResource("bu_account");
                lbl_lastpayment.Content = temp.lastpay.ToString("dd-MM-yyyy");
            }
            else
            {
                lbl_lastpayment.Content = (string)FindResource("lifetime_license_account");
            }
            lbl_name.Content = temp.firstName + " " + temp.lastName;

        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            string accountString = "update accounts set ";
            bool updateable = true;

            if (tb_firstname.Text != "")
                accountString += "firstname = '" + tb_firstname.Text + "', ";
            else
                updateable = false;

            if (tb_lastname.Text != "")
                accountString += "lastname = '" + tb_lastname.Text + "', ";
            else
                updateable = false;

            if (tb_mail.Text != "")
                accountString += "email = '" + tb_mail.Text + "'";
            else
                updateable = false;

            if (tb_password.Password != "")
            {
                if (tb_password.Password == tb_confpassword.Password)
                    accountString += ", password = '" + Certify.Encrypt(tb_password.Password) + "'";
            }

            if (new TextRange(tb_desc.Document.ContentStart, tb_desc.Document.ContentEnd).Text != "'")
                accountString += ", description = '" + new TextRange(tb_desc.Document.ContentStart, tb_desc.Document.ContentEnd).Text + "'";

            if (!worker_saveAccountSettings.IsBusy && updateable) {
                btn_save.IsEnabled = false;
                accountString += " where acc_id = " + MainWindow.account.id + ";";
                worker_saveAccountSettings.RunWorkerAsync(accountString);
            }
        }

        private void btn_upgrade_Click(object sender, RoutedEventArgs e)
        {
            if(!worker_upgradeAccount.IsBusy) {
                btn_upgrade.IsEnabled = false;
                worker_upgradeAccount.RunWorkerAsync();
            }
        }
    }
}
