using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace TMS.views
{
    /// <summary>
    /// Interaction logic for ProjectView.xaml
    /// </summary>
    public partial class ProjectView : UserControl
    {
        private SettingsView settingsView { get; set; }
        public AddView addView { get; set; }
        public ObservableCollection<Task> AllTasks { get; set; }
        private List<string> allProjects = new List<string>();

        public ProjectView()
        {
            InitializeComponent();
            dgrid.ItemsSource = InitializeAllTasks();
            addView = new AddView();
            settingsView = new SettingsView();
            this.gb_settings.Content = this.settingsView;
            this.gb_add.Content = this.addView;

            this.allProjects.Add("Project 1");
            this.allProjects.Add("Project 2");
            this.allProjects.Add("Project 3");
            this.allProjects.Add("Project 4");
            this.allProjects.Add("Project 5");
            this.allProjects.Add("Project 6");
            this.allProjects.Add("Project 7");
            this.allProjects.Add("Project 8");
            this.allProjects.Add("Project 9");
            this.allProjects.Add("Project 10");
            this.allProjects.Add("Project 11");
            this.allProjects.Add("Project 12");
            this.dropdown_allprojects.ItemsSource = this.allProjects;
            this.dropdown_allprojects.SelectedIndex = 0;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Task theObject = new Task();
            theObject.IsMarked = false;
            if (addView.Descripition != null)
                theObject.Name = addView.Descripition;
            else
            {
                //MainWindow.instance.ShowMessageDialog(this, null);
                return;
            }

            if (addView.Deadline != null)
                theObject.Deadline = (DateTime)addView.Deadline;
            else
                return;

            theObject.Assignees = "";

            addView.ClearAllControls();
            AllTasks.Add(theObject);
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
            theObject.Name = "The object " + i;
            theObject.Deadline = DateTime.Now;
            theObject.Assignees = "Michi, Nenad, ...";
            return theObject;
        }

        private void dgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.settingsView.SelectionChanged(dgrid.SelectedIndex);
            
            settingsView.SetNewTask(AllTasks[settingsView._currentIndex]);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (settingsView._currentIndex != -1)
            {

                AllTasks.RemoveAt(dgrid.SelectedIndex);
                settingsView._currentIndex = -1;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            object obj = parentWindow.FindName("flyout");
            Flyout flyout = (Flyout)obj;
            object lbl = parentWindow.FindName("lbl_proj");
            Label projlbl = (Label)lbl;
            projlbl.Content = "History of " + this.dropdown_allprojects.SelectedItem.ToString();
            flyout.IsOpen = !flyout.IsOpen;
        }
    }
}
