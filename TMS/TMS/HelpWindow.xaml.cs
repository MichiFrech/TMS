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

namespace TMS
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : MetroWindow
    {
        public HelpWindow()
        {
            InitializeComponent();
            textBox.Text = "TODO\n";
            textBox.Text += "TODO";//TODO
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
