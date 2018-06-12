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
        public string Name { get { return tb_name.Text; } }
        public DateTime? Deadline { get { return dp_deadline.SelectedDate; } }
        public string Desc { get { return new TextRange(tb_desc.Document.ContentStart, tb_desc.Document.ContentEnd).Text; } }

        public SettingsView(string assignees)
        {
            InitializeComponent();
            this._currentIndex = -1;

            temp2 = assignees.Split(',').ToList();
            for (int i = 0; i < temp2.Count; i++)
            {
                temp2[i] = temp2[i].TrimStart();
            }

            listView.ItemsSource = temp2;
            this.task_repetition.SelectedIndex = 0;

            dp_deadline.DisplayDateStart = DateTime.Now;
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
            this.dp_deadline.SelectedDate = DateTime.ParseExact(task.Deadline, "dd-MM-yyyy", null);
            listView.SelectedItems.Clear();
            for (int i = 0; i < temp2.Count; i++)
            {
                if (task.Assignees.Contains(temp2[i]))
                {
                    listView.SelectedItems.Add(listView.Items[i]);
                }
            }
            TextRange textRange = new TextRange(tb_desc.Document.ContentStart, tb_desc.Document.ContentEnd);
            if(task.Desc != null)
                textRange.Text = task.Desc;
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
