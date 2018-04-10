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
using MahApps.Metro;

namespace TMS.views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        private MainWindow mwinstance;

        public AccountView()
        {
            InitializeComponent();
            this.mwinstance = MainWindow.instance;
            if (Properties.Settings.Default.Theme == "BaseLight")
                this.currentTheme.IsChecked = false;
            else
                this.currentTheme.IsChecked = true;

            SetDescText();
        }

        private void SetDescText()
        {
            FlowDocument myFlowDoc = new FlowDocument();

            // Add paragraphs to the FlowDocument.
            for (int i = 0; i < 20; i++)
                myFlowDoc.Blocks.Add(new Paragraph(new Run("Paragraph " + (i + 1))));

            tb_desc.Document = myFlowDoc;
            tb_desc.SetValue(Paragraph.LineHeightProperty, 1.0);
        }

        private void currentTheme_Click(object sender, RoutedEventArgs e)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, ThemeManager.GetAppTheme((bool)currentTheme.IsChecked ? "BaseDark" : "BaseLight"));

            Properties.Settings.Default.Theme = (bool)currentTheme.IsChecked ? "BaseDark" : "BaseLight";
            Properties.Settings.Default.Save();
        }
    }
}
