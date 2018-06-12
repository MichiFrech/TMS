using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using MahApps.Metro.IconPacks;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro;
using Mvvm.Async;
using System.ComponentModel;
using System.Data.SqlClient;

namespace TMS
{
    /// <summary>
    /// Interaction logic for MainProjectWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static Account account;
        public static MainWindow instance;
        private int selectedIndex;
        private string historyInsertString;

        #region Database
        private string connectionString = "workstation id=tmsdatabase.mssql.somee.com;packet size=4096;" +
                "user id=mfrech_SQLLogin_1;pwd=x9v9kqzi71;data source=tmsdatabase.mssql.somee.com;" +
                "persist security info=False;initial catalog=tmsdatabase";
        private SqlConnection cnn;
        BackgroundWorker worker;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.ShowIconOnTitleBar = false;
            this.HamburgerMenuControl.SelectedIndex = 0;
            this.selectedIndex = 0;
            MainWindow.instance = this;
            cnn = new SqlConnection(connectionString);
            worker = new BackgroundWorker();
            worker.DoWork += Worker_AddProjectToDatabase;

            Properties.Settings.Default.RememberUserName = Certify.Encrypt(MainWindow.account.email);
            Properties.Settings.Default.Save();
        }

        private async void ShowMessage()
        {
            await this.ShowMessageAsync("Welcome to TMS!", "Click on 'Create Project' for creating your first project.", MessageDialogStyle.Affirmative);
        }

        private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs e)
        {
            if(e.ClickedItem == item_logout) {
                FrontPage newFp = new FrontPage();
                newFp.Show();
                this.Close();
            } else {
                // set the content
                this.HamburgerMenuControl.Content = e.ClickedItem;
                selectedIndex = this.HamburgerMenuControl.SelectedIndex;
            }

            // close the pane
            this.HamburgerMenuControl.IsPaneOpen = false;
        }

        private void Btn_window_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://tmsproject.somee.com");
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Firstuse == true) {
                ShowMessage();
                Properties.Settings.Default.Firstuse = false;
                Properties.Settings.Default.Save();
            }
        }

        private void Button_OkForNewProject_Click(object sender, RoutedEventArgs e)
        {

            string name = tbNewProjectName.Text;


            string deadline = dpDueDateOfNewProject.Text;
            if (deadline == "")
                deadline = "2099-12-31";
            else
            {
                DateTime temp = DateTime.Parse(deadline);
                deadline = temp.ToString("yyyy-MM-dd");
            }

            int ownerAndAssignee = MainWindow.account.id;

            if (name != null && name != "")
            {
                string stringForSQL = "'" + name + "', CONVERT(date, '" + deadline + "'), " + ownerAndAssignee + ", " + ownerAndAssignee;

                if (!worker.IsBusy)
                {
                    worker.RunWorkerAsync(stringForSQL);
                }
                tbNewProjectName.Text = "";
                dpDueDateOfNewProject.Text = "";
                flyout_NewProject.IsOpen = false;
            }

        }

        private void Worker_AddProjectToDatabase(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand cmd = cnn.CreateCommand();

                cmd.CommandText = "INSERT INTO projects (name, duedate, assignees, owner) VALUES (" + e.Argument.ToString() + ");";

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
