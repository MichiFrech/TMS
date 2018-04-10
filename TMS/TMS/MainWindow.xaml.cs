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

        public MainWindow()
        {
            InitializeComponent();
            this.ShowIconOnTitleBar = false;
            this.HamburgerMenuControl.SelectedIndex = 0;
            this.selectedIndex = 0;
            MainWindow.instance = this;
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
    }
}
