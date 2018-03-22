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

namespace TMS.views
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddView : UserControl
    {
        public List<string> temp2 = new List<string>();

        public string Descripition
        {
            get
            {
                if (this.tb_desc.Text != null && this.tb_desc.Text != "")
                    return this.tb_desc.Text;
                return null;
            }
        }

        public DateTime? Deadline
        {
            get
            {
                if (this.dp_deadline.SelectedDate != null)
                    return (DateTime)this.dp_deadline.SelectedDate;
                return null;
            }
        }

        public AddView()
        {
            InitializeComponent();
            temp2.Add("Nenad");
            temp2.Add("Michael");
            temp2.Add("Usw.");
            temp2.Add("Usw.");
            temp2.Add("Usw.");
            temp2.Add("Usw.");
            temp2.Add("Usw.");
            temp2.Add("Usw.");
            temp2.Add("Usw.");
            temp2.Add("Usw.");
            temp2.Add("Usw.");

            listView.ItemsSource = temp2;
        }

        public void ClearAllControls()
        {
            tb_desc.Text = "";
            dp_deadline.SelectedDate = null;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: Add selected items to string
        }
    }
}
