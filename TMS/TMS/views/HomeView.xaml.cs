using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public ObservableCollection<Task> AllTasks { get; set; }

        public HomeView()
        {
            InitializeComponent();
            dgrid_today.ItemsSource = InitializeAllTasks();
            dgrid_week.ItemsSource = InitializeAllTasks();
            dgrid_month.ItemsSource = InitializeAllTasks();
        }

        public ObservableCollection<Task> InitializeAllTasks()
        {
            AllTasks = new ObservableCollection<Task>();
            for (int i = 0; i < 5; i++)
            {
                AllTasks.Add(InitializeMyObject(i + 1));
            }
            return AllTasks;
        }

        public Task InitializeMyObject(int i)
        {
            Task theObject = new Task();
            theObject.IsMarked = true;
            theObject.Name = "Task " + i;
            theObject.Deadline = DateTime.Now;
            theObject.Assignees = "Michi, Nenad, ...";
            return theObject;
        }
    }
}
