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
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public List<string> temp2 = new List<string>();
        public int _currentIndex;
        public Task task;

        public SettingsView()
        {
            InitializeComponent();
            this._currentIndex = -1;

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
            this.task_repetition.SelectedIndex = 0;
        }

        public void SelectionChanged(int index)
        {
            this._currentIndex = index;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: Add selected items to string
        }

        public void SetNewTask(Task newTask)
        {
            this.task = newTask;

            this.tb_name.Text = task.Name;
            this.dp_deadline.SelectedDate = task.Deadline;

            //TODO: Set fields
        }

        private void task_repetition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(task_repetition.SelectedIndex == 4) {
                calendar.IsEnabled = true;
            } else {
                calendar.IsEnabled = false;
            }
        }
    }
}
