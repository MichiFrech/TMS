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
using LiveCharts;
using LiveCharts.Wpf;

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

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Tasks",
                    Values = new ChartValues<double> { 4, 6, 5, 2 , 7, 4, 6, 5, 2 , 7, 4, 6, 5, 2 , 7, 4, 6, 5, 2 , 7, 4, 6, 5, 2 , 7, 4, 6, 5, 2 , 7, 3, 5 },
                    LineSmoothness = 0
                }
            };

            Labels = new[] { "DAY 1", "DAY 2", "DAY 3", "DAY 4", "DAY 5", "DAY 6", "DAY 7", "DAY 8", "DAY 9", "DAY 10", "DAY 11", "DAY 12", "DAY 13", "DAY 14", "DAY 15", "DAY 16", "DAY 17", "DAY 18", "DAY 19", "DAY 20", "DAY 21", "DAY 22", "DAY 23", "DAY 24", "DAY 25", "DAY 26", "DAY 27", "DAY 28", "DAY 29", "DAY 30", "DAY 31", "DAY 32" };
            YFormatter = value => value.ToString("");

            //modifying the series collection will animate and update the chart

            //modifying any series values will also animate and update the chart

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

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
