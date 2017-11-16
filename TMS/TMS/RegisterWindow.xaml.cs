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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using System.IO;

namespace TMS
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class RegisterWindow : MetroWindow
    {
        public RegisterWindow()
        {
            InitializeComponent();
            this.ShowIconOnTitleBar = false;
            tb_firstname.Focus();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void gobackbtn_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            Register();
        }

        private void Register()
        {
            if (firstname_state_icon.Kind == PackIconMaterialKind.CheckCircleOutline &&
                lastname_state_icon.Kind == PackIconMaterialKind.CheckCircleOutline &&
                mail_state_icon.Kind == PackIconMaterialKind.CheckCircleOutline &&
                password_state_icon.Kind == PackIconMaterialKind.CheckCircleOutline) //Check if the informations are correct
            {
                if (PutAccountIntoSystem(tb_firstname.Text, tb_lastname.Text, tb_mail.Text, tb_pw.Password))
                {
                    lbl_msg.Content = "Successfully registered!";
                    lbl_msg.Foreground = Brushes.Green;
                    lbl_msg.Visibility = Visibility.Visible;

                    return;
                }
            }

            lbl_msg.Content = "An error has occurred!";
            lbl_msg.Foreground = Brushes.Red;
            lbl_msg.Visibility = Visibility.Visible;
        }

        private void GoBack()
        {
            FrontPage fp = new FrontPage();
            fp.Show();
            this.Close();
        }

        private void tb_firstname_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityFirstName();
        }

        private void tb_lastname_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityLastName();
        }

        private void tb_mail_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityMail();
        }

        private void tb_pw_LostFocus(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityPW();
        }
        private void ChangeVisibilityFirstName()
        {
            if (!Certify.CheckName(tb_firstname.Text))
            {
                firstname_state_icon.Kind = PackIconMaterialKind.CloseCircleOutline;
                firstname_state_icon.ToolTip = "Only Letters are allowed";
            }
            else
            {
                firstname_state_icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            }
        }

        private void ChangeVisibilityLastName()
        {
            if (!Certify.CheckName(tb_lastname.Text))
            {
                lastname_state_icon.Kind = PackIconMaterialKind.CloseCircleOutline;
                lastname_state_icon.ToolTip = "The field can not be empty";
            }
            else
            {
                lastname_state_icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            }
        }

        private void ChangeVisibilityPW()
        {
            if (!Certify.CheckPassword(tb_pw.Password))
            {
                password_state_icon.Kind = PackIconMaterialKind.CloseCircleOutline;
                password_state_icon.ToolTip = "At least one upper case letter • At least one lower case letter • At least one digit • Minimum 8 in length";
            }
            else
            {
                password_state_icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            }
        }

        private void ChangeVisibilityMail()
        {
            if (!Certify.CheckMail(tb_mail.Text))
            {
                mail_state_icon.Kind = PackIconMaterialKind.CloseCircleOutline;
                mail_state_icon.ToolTip = "The email address must be in the format name@mail.com";
            }
            else
            {
                mail_state_icon.Kind = PackIconMaterialKind.CheckCircleOutline;
            }
        }

        private bool PutAccountIntoSystem(string firstname, string lastname, string email, string password)     //Write Account into Database
        {
            try
            {
                string accLine = firstname + ";" + lastname + ";" + email + ";" + Certify.Encrypt(password);
                if (File.Exists(Directory.GetCurrentDirectory() + "/Accounts.csv"))
                {
                    bool isRegistered = false;
                    string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Accounts.csv");
                    for (int i = 0; i < lines.Length; i++) {
                        string[] temp = lines[i].Split(';');
                        if (email == temp[3])
                            isRegistered = true;
                    }

                    if (!isRegistered) {
                        string[] curline = new string[1];
                        int id = lines.Length;
                        if (lines.Length == Convert.ToInt16(lines[lines.Length - 1].Split(';')[0]))
                            id++;
                        curline[0] = lines.Length + ";" + accLine;
                        File.AppendAllLines(Directory.GetCurrentDirectory() + "/Accounts.csv", curline);
                        return true;
                    } else {
                        return false;
                    }
                }
                else {
                    string[] curline = new string[2];
                    curline[0] = "Id;FirstName;LastName;Email;Password";
                    curline[1] = "1;" + accLine;
                    File.WriteAllLines(Directory.GetCurrentDirectory() + "/Accounts.csv", curline);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
