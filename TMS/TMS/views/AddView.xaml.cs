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

        public string Name
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

        public AddView(string assignees)
        {
            InitializeComponent();
            temp2 = assignees.Split(',').ToList();
            for (int i = 0; i < temp2.Count; i++)
            {
                temp2[i] = temp2[i].TrimStart();
            }

            listView.ItemsSource = temp2;

            dp_deadline.DisplayDateStart = DateTime.Now;
        }

        public void ClearAllControls()
        {
            tb_desc.Text = "";
            dp_deadline.SelectedDate = null;
            listView.SelectedItems.Clear();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: Add selected items to string
        }
    }
}
