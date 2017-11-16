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
using MahApps.Metro.IconPacks;
using MahApps.Metro.Controls.Dialogs;

namespace TMS
{
    /// <summary>
    /// Interaction logic for MainProjectWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static Account account;
        public static MainWindow instance;
        public MainWindow()
        {
            InitializeComponent();
            this.ShowIconOnTitleBar = false;
            this.HamburgerMenuControl.SelectedIndex = 0;
            MainWindow.instance = this;
        }

        private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs e)
        {
            // set the content
            this.HamburgerMenuControl.Content = e.ClickedItem;
            // close the pane
            this.HamburgerMenuControl.IsPaneOpen = false;
        }
    }
}
