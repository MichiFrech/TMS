using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;
using MahApps.Metro;
using System.Threading;
using System.Globalization;
using System.Windows.Markup;

namespace TMS
{
    /// <summary>
    /// Interaction logic for FrontPage.xaml
    /// </summary>
    public partial class FrontPage : MetroWindow
    {
        private string currentCulture;

        public FrontPage()
        {
            InitializeComponent();
            currentCulture = Properties.Settings.Default.Lang;
            if (currentCulture == "en-US")
            {
                comboBox.SelectedIndex = 0;
            }
            else
            {
                comboBox.SelectedIndex = 1;
            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(currentCulture);
            this.SetLanguageDictionary();

            var theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, ThemeManager.GetAppTheme(Properties.Settings.Default.Theme));
        }

        private void Btn_login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void Btn_register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow reg = new RegisterWindow();
            reg.Show();
            this.Close();
        }

        private void Btn_window_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://tmsproject.somee.com");
        }

        private void SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en-US":
                    dict.Source = new Uri("..\\Resources\\StringResources.en-US.xaml", UriKind.Relative);
                    break;
                case "de-DE":
                    dict.Source = new Uri("..\\Resources\\StringResources.de-DE.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\StringResources.en-US.xaml", UriKind.Relative);
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Add(dict);

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = comboBox.SelectedIndex;
            if (index == 0)
                currentCulture = "en-US";
            else
                currentCulture = "de-DE";

            Properties.Settings.Default.Lang = currentCulture;
            Properties.Settings.Default.Save();

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(currentCulture);

            this.SetLanguageDictionary();
        }
    }
}