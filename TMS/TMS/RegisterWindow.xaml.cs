using System;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Input;

namespace TMS
{
    public partial class RegisterWindow : MetroWindow
    {
        private Thread thread;
        private volatile bool threadHasToStop = false;
        private volatile bool sqlStatementIsRunning = false;
        private volatile string firstname = "";
        private volatile string lastname = "";
        private volatile string mail = "";
        private volatile string password = "";
        private volatile string businessusage = "FALSE";
        private string connectionString = "workstation id=tmsdatabase.mssql.somee.com;packet size=4096;" +
                "user id=mfrech_SQLLogin_1;pwd=x9v9kqzi71;data source=tmsdatabase.mssql.somee.com;" +
                "persist security info=False;initial catalog=tmsdatabase";
        private SqlConnection cnn;

        public RegisterWindow()
        {
            InitializeComponent();
            this.ShowIconOnTitleBar = false;
            tb_firstname.Focus();

            cnn = new SqlConnection(connectionString);
            thread = new Thread(RunStatement);
            thread.Start();
        }

        private void gobackbtn_Click(object sender, RoutedEventArgs e)
        {
            FrontPage fp = new FrontPage();
            fp.Show();
            this.Close();
        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            Register();
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Register();
            }
        }

        private bool CheckInfos()
        {
            return firstname_state_icon.Kind == PackIconMaterialKind.CheckCircleOutline &&
                lastname_state_icon.Kind == PackIconMaterialKind.CheckCircleOutline &&
                mail_state_icon.Kind == PackIconMaterialKind.CheckCircleOutline &&
                password_state_icon.Kind == PackIconMaterialKind.CheckCircleOutline;
        }

        private void Register()
        {
            ChangeVisibility();

            if (CheckInfos()) //Check if the informations are correct
            {
                bool temp = (bool)rb_bu.IsChecked;
                businessusage = temp ? "TRUE" : "FALSE";
                firstname = tb_firstname.Text;
                lastname = tb_lastname.Text;
                mail = tb_mail.Text;
                password = tb_pw.Password;

                threadHasToStop = false;
                sqlStatementIsRunning = true;
                progring.IsActive = true;
            }
            else
            {
                lbl_msg.Content = (string)FindResource("msg_err");
                lbl_msg.Foreground = Brushes.Red;
                lbl_msg.Visibility = Visibility.Visible;
            }
        }

        private void Lostfocus(object sender, RoutedEventArgs e)
        {
            ChangeVisibility();
        }

        private void ChangeVisibility()
        {
            if (!Certify.CheckName(tb_firstname.Text)) {
                firstname_state_icon.Kind = PackIconMaterialKind.CloseCircleOutline;
            } else {
                firstname_state_icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            }
            if (!Certify.CheckName(tb_lastname.Text)) {
                lastname_state_icon.Kind = PackIconMaterialKind.CloseCircleOutline;
            } else {
                lastname_state_icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            }
            if (!Certify.CheckPassword(tb_pw.Password)) {
                password_state_icon.Kind = PackIconMaterialKind.CloseCircleOutline;
            } else {
                password_state_icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            }
            if (!Certify.CheckMail(tb_mail.Text)) {
                mail_state_icon.Kind = PackIconMaterialKind.CloseCircleOutline;
            } else {
                mail_state_icon.Kind = PackIconMaterialKind.CheckCircleOutline;
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
                        bool registered = false;
                        SqlCommand command = cnn.CreateCommand();
                        command.CommandText = "select email from accounts;";
                        SqlDataReader reader;

                        cnn.Open();

                        reader = command.ExecuteReader();
                        while (reader.Read())
                            for (int i = 0; i < reader.FieldCount; i++)
                                if (mail == reader.GetValue(i).ToString())
                                    registered = true;

                        if(!registered)
                        {
                            int maxID = 0;
                            command.CommandText = "select top 1 id from accounts order by id desc;";

                            reader.Close();
                            reader = command.ExecuteReader();
                            while (reader.Read())
                                for (int i = 0; i < reader.FieldCount; i++)
                                    maxID = Convert.ToInt16(reader.GetValue(i).ToString()) + 1;

                            command.CommandText = "insert into accounts values('" + maxID + "', '" + firstname + "', '" + lastname + "', '" + mail + "', '" + Certify.Encrypt(password) + "', '" + businessusage + "', CONVERT (date, GETDATE()), null);";

                            reader.Close();
                            command.ExecuteNonQuery();

                            Dispatcher.Invoke(new Action(() =>
                            {
                                lbl_msg.Content = (string)FindResource("msg_succ");
                                lbl_msg.Foreground = Brushes.Green;
                                lbl_msg.Visibility = Visibility.Visible;
                                progring.IsActive = false;

                                threadHasToStop = true;
                            }));
                        }
                    } catch (Exception ex) {

                    }
                }
            }
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            threadHasToStop = true;
            if (thread.IsAlive)
                thread.Abort();
        }
    }
}
