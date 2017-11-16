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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        private Account acc;
        public LoginWindow()
        {
            InitializeComponent();
            this.ShowIconOnTitleBar = false;
            tb_mail.Focus();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityPW();
            Login();
        }

        private void tb_mail_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityMail();
        }

        private void tb_mail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ChangeVisibilityMail();
                Login();
            }
        }

        private void tb_pw_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityPW();
        }

        private void tb_pw_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                ChangeVisibilityPW();
                Login();
            }
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void Login()
        {
            if (mail_state_icon.Kind == PackIconMaterialKind.CheckCircleOutline && 
                password_state_icon.Kind == PackIconMaterialKind.CheckCircleOutline)
            {
                if (CorrectAccountinforamtions(tb_mail.Text, tb_pw.Password))  //if login data is correct
                {
                    MainWindow.account = acc;
                    MainWindow mpw = new MainWindow();
                    mpw.Show();
                    this.Close();
                }
            }

            lbl_msg.Content = "An error has occurred!";
            lbl_msg.Foreground = Brushes.Red;
            lbl_msg.Visibility = Visibility.Visible;
        }

        private void ChangeVisibilityPW()
        {
            if (!Certify.CheckPassword(tb_pw.Password)) {
                password_state_icon.Kind = PackIconMaterialKind.CloseCircleOutline;
                password_state_icon.ToolTip = "At least one upper case letter • At least one lower case letter • At least one digit • Minimum 8 in length";
            } else {
                password_state_icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            }
        }

        private void ChangeVisibilityMail()
        {
            if (!Certify.CheckMail(tb_mail.Text)) {
                mail_state_icon.Kind = PackIconMaterialKind.CloseCircleOutline;
                mail_state_icon.ToolTip = "The email address must be in the format name@mail.com";
            } else {
                mail_state_icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            }
        }

        private void GoBack()
        {
            FrontPage fp = new FrontPage();
            fp.Show();
            this.Close();
        }

        private bool CorrectAccountinforamtions(string email, string password)
        {
            try
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "/Accounts.csv"))
                {
                    string[] temp = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Accounts.csv");
                    bool isRegistered = false;
                    for (int i = 0; i < temp.Length; i++)
                    {
                        string[] currentLine = temp[i].Split(';');
                        if (email == currentLine[3] && password == Certify.Decrypt(currentLine[4]))
                        {
                            isRegistered = true;
                            acc = new Account(temp[i]);
                        }
                    }

                    return isRegistered;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
