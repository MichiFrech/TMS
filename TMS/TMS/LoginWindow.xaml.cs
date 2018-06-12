using System;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Threading;

namespace TMS
{
    public partial class LoginWindow : MetroWindow
    {
        private Thread thread;
        private volatile bool threadHasToStop = false;
        private volatile bool sqlStatementIsRunning = false;
        private volatile string mail = "";
        private volatile string password = "";
        private volatile string accountLine;
        private string connectionString = "workstation id=tmsdatabase.mssql.somee.com;packet size=4096;" +
                "user id=mfrech_SQLLogin_1;pwd=x9v9kqzi71;data source=tmsdatabase.mssql.somee.com;" +
                "persist security info=False;initial catalog=tmsdatabase";
        private SqlConnection cnn;

        public LoginWindow()
        {
            InitializeComponent();
            this.ShowIconOnTitleBar = false;
            
            if(Properties.Settings.Default.RememberUserName != null && Properties.Settings.Default.RememberUserName != "") {
                tb_mail.Text = Certify.Decrypt(Properties.Settings.Default.RememberUserName);
                tb_pw.Focus();
            }
            else
            {
                tb_mail.Focus();
            }
            

            cnn = new SqlConnection(connectionString);
            thread = new Thread(RunStatement);
            thread.Start();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeVisibility();
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            FrontPage fp = new FrontPage();
            fp.Show();
            this.Close();
        }

        private bool CheckInfos()
        {
            return mail_state_icon.Kind == PackIconMaterialKind.CheckCircleOutline &&
                password_state_icon.Kind == PackIconMaterialKind.CheckCircleOutline;
        }

        private void Login()
        {
            ChangeVisibility();

            if (CheckInfos())
            {
                mail = tb_mail.Text;
                password = tb_pw.Password;

                threadHasToStop = false;
                sqlStatementIsRunning = true;
                progring.IsActive = true;
            }
            else
            {
                lbl_msg.Foreground = Brushes.Red;
                lbl_msg.Visibility = Visibility.Visible;
            }
        }

        private void ChangeVisibility()
        {
            if (!Certify.CheckMail(tb_mail.Text)) {
                mail_state_icon.Kind = PackIconMaterialKind.CloseCircleOutline;
            } else {
                mail_state_icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            }
            if (!Certify.CheckPassword(tb_pw.Password)) {
                password_state_icon.Kind = PackIconMaterialKind.CloseCircleOutline;
            } else {
                password_state_icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            }
        }

        private void RunStatement()
        {
            while (!threadHasToStop)
            {
                if (sqlStatementIsRunning)
                {
                    sqlStatementIsRunning = false;

                    try
                    {
                        SqlCommand command = cnn.CreateCommand();
                        command.CommandText = "select acc_id, firstname, lastname, email, password, businessusage, CONVERT(varchar, lastpayment, 105), description from accounts where email='" + mail + "';";
                        SqlDataReader reader;
                        bool correctInformations = false;
                        string accLine = "";

                        cnn.Open();

                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            for (short i = 0; i < reader.FieldCount; i++)
                            {
                                if (i == 4 && Certify.Decrypt(reader.GetValue(i).ToString()) == password)
                                    correctInformations = true;

                                accLine += reader.GetValue(i).ToString() + ';';
                            }
                        }

                        cnn.Close();
                        Dispatcher.Invoke(new Action(() => {
                            progring.IsActive = false;

                            if (correctInformations)
                            {
                                accountLine = accLine;
                                MainWindow.account = new Account(accountLine);
                                MainWindow mpw = new MainWindow();
                                mpw.Show();
                                this.Close();
                            } else {
                                lbl_msg.Foreground = Brushes.Red;
                                lbl_msg.Visibility = Visibility.Visible;
                            }
                            
                        }));
                    } catch (Exception ex) {

                    }
                }
            }
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            threadHasToStop = true;
            if(thread.IsAlive)
                thread.Abort();
        }
    }
}
