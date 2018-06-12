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
using System.Data.SqlClient;
using System.ComponentModel;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace TMS.views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public ObservableCollection<Task> AllTasksToday { get; set; }
        public ObservableCollection<Task> AllTasksWeek { get; set; }
        public ObservableCollection<Task> AllTasksMonth { get; set; }
        public ObservableCollection<Notification> AllNotifications { get; set; }

        #region Database
        private string connectionString = "workstation id=tmsdatabase.mssql.somee.com;packet size=4096;" +
                "user id=mfrech_SQLLogin_1;pwd=x9v9kqzi71;data source=tmsdatabase.mssql.somee.com;" +
                "persist security info=False;initial catalog=tmsdatabase";
        private SqlConnection cnn;
        BackgroundWorker worker;
        BackgroundWorker worker_statechanged;
        BackgroundWorker worker_notifications;
        BackgroundWorker worker_invitation;
        BackgroundWorker worker_deleteNotification;
        BackgroundWorker worker_AddToHistory;
        string currentList = "today";
        string historyInsert = "";
        int proj_id = -1;
        int tempIndex = -1;
        bool addEntry = false;
        #endregion

        #region Chart
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        #endregion

        public HomeView()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection {
                new LineSeries {
                    Title = (string)FindResource("task_stat"),
                    Values = new ChartValues<double> { 4, 6, 5, 2 , 7, 4, 6, 5, 2 , 7, 4, 6, 5, 2 , 7, 4, 6, 5, 2 , 7, 4, 6, 5, 2 , 7, 4, 6, 5, 2 , 7, 3, 5 },
                    LineSmoothness = 0
                }
            };
            string day = (string)FindResource("day");
            Labels = new[] { day + " 1", day + " 2", day + " 3", day + " 4", day + " 5", day + " 6", day + " 7", day + " 8", day + " 9", day + " 10", day + " 11", day + " 12", day + " 13", day + " 14", day + " 15", day + " 16", day + " 17", day + " 18", day + " 19", day + " 20", day + " 21", day + " 22", day + " 23", day + " 24", day + " 25", day + " 26", day + " 27", day + " 28", day + " 29", day + " 30", day + " 31", day + " 32" };
            YFormatter = value => value.ToString("");

            //modifying the series collection will animate and update the chart

            //modifying any series values will also animate and update the chart

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker_statechanged = new BackgroundWorker();
            worker_statechanged.DoWork += Worker_statechanged_DoWork;
            worker_statechanged.RunWorkerCompleted += Worker_statechanged_RunWorkerCompleted;
            worker_notifications = new BackgroundWorker();
            worker_notifications.DoWork += Worker_notifications_DoWork;
            worker_notifications.RunWorkerCompleted += Worker_notifications_RunWorkerCompleted;
            worker_invitation = new BackgroundWorker();
            worker_invitation.DoWork += Worker_invitation_DoWork;
            worker_invitation.RunWorkerCompleted += Worker_invitation_RunWorkerCompleted;
            worker_deleteNotification = new BackgroundWorker();
            worker_deleteNotification.DoWork += Worker_deleteNotification_DoWork;
            worker_AddToHistory = new BackgroundWorker();
            worker_AddToHistory.DoWork += Worker_AddToHistory_DoWork;
            worker_AddToHistory.RunWorkerCompleted += Worker_AddToHistory_RunWorkerCompleted;

            DataContext = this;
            cnn = new SqlConnection(connectionString);
            worker.RunWorkerAsync();
        }

        private void Worker_AddToHistory_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!worker_deleteNotification.IsBusy) {
                worker_deleteNotification.RunWorkerAsync(AllNotifications[tempIndex].notification_id);

                AllNotifications.RemoveAt(tempIndex);
            }
            addEntry = false;
        }

        private void Worker_AddToHistory_DoWork(object sender, DoWorkEventArgs e)
        {
            if (addEntry)
            {
                try
                {
                    SqlCommand command = cnn.CreateCommand();
                    command.CommandText = "select name from projects where proj_id = " + proj_id;
                    SqlDataReader reader;
                    string name = "";

                    cnn.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        name += reader.GetValue(0);
                    }
                    reader.Close();

                    command = cnn.CreateCommand();

                    command.CommandText = e.Argument.ToString() + name + "\" by admin', CONVERT (date, '" + DateTime.Now.ToString("yyyy-MM-dd") + "'));";

                    if (cnn.State != System.Data.ConnectionState.Open)
                        cnn.Open();
                    command.ExecuteNonQuery();
                    cnn.Close();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void Worker_deleteNotification_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();

                if(Convert.ToInt16(e.Argument) != -1)
                {
                    cnn.Open();

                    command.CommandText = "delete from notifications where notification_id = " + Convert.ToInt16(e.Argument) + ";";
                    command.ExecuteNonQuery();

                    cnn.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_invitation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!worker_AddToHistory.IsBusy)
                worker_AddToHistory.RunWorkerAsync(historyInsert);
        }

        private void Worker_invitation_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();
                int index = Convert.ToInt16(e.Argument.ToString());
                command.CommandText = "select assignees from projects where proj_id = " + proj_id;
                SqlDataReader reader;
                string assignees = "";

                if(cnn.State != System.Data.ConnectionState.Open)
                    cnn.Open();
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    assignees += reader.GetValue(0);
                }
                reader.Close();

                if (! assignees.Contains(MainWindow.account.id.ToString()))
                {
                    command.CommandText = "update projects set assignees = assignees + '," + MainWindow.account.id + "' where proj_id = " + proj_id + ";";
                    command.ExecuteNonQuery();
                }

                cnn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_notifications_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lv_inbox.ItemsSource = AllNotifications;
        }

        private void Worker_notifications_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();
                command.CommandText = "select * from notifications where acc_id like '%" + MainWindow.account.id + "%';";
                SqlDataReader reader;
                AllNotifications = new ObservableCollection<Notification>();

                cnn.Open();
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    string t = "";
                    for (short i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.GetValue(i).ToString() == "")
                            t += "-1";
                        else
                            t += reader.GetValue(i);
                        if (i != reader.FieldCount - 1)
                            t += ";";
                    }
                    string[] arr = t.Split(';');
                    AllNotifications.Add(new Notification(Convert.ToInt16(arr[0]), arr[1], arr[2], Convert.ToInt16(arr[3])));
                }

                cnn.Close();
                reader.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_statechanged_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            worker.RunWorkerAsync();
        }

        private void Worker_statechanged_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();
                int doneVal = -1;
                int taskID = -1;
                switch (currentList)
                {
                    case "today":
                        doneVal = Convert.ToInt16(AllTasksToday[Convert.ToInt16(e.Argument)].Done);
                        taskID = AllTasksToday[Convert.ToInt16(e.Argument)].Task_id;
                        break;
                    case "week":
                        doneVal = Convert.ToInt16(AllTasksWeek[Convert.ToInt16(e.Argument)].Done);
                        taskID = AllTasksWeek[Convert.ToInt16(e.Argument)].Task_id;
                        break;
                    case "month":
                        doneVal = Convert.ToInt16(AllTasksMonth[Convert.ToInt16(e.Argument)].Done);
                        taskID = AllTasksMonth[Convert.ToInt16(e.Argument)].Task_id;
                        break;
                }
                
                command.CommandText = "update tasks set done = " + doneVal + " where task_id = " + taskID + ";";

                cnn.Open();

                command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgrid_today.ItemsSource = AllTasksToday;
            dgrid_week.ItemsSource = AllTasksWeek;
            dgrid_month.ItemsSource = AllTasksMonth;

            if (!worker_notifications.IsBusy)
                worker_notifications.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlDataReader reader;
                List<Task> tempListToday = new List<Task>();
                List<Task> tempListWeek = new List<Task>();
                List<Task> tempListMonth = new List<Task>();


                cnn.Open();

                reader = ReturnTodayAsSqlCommand().ExecuteReader();
                while (reader.Read())
                {
                    string t = "";
                    for (short i = 0; i < reader.FieldCount; i++)
                    {
                        t += reader.GetValue(i);
                        if (i != reader.FieldCount - 1)
                            t += ";";
                    }
                    string[] arr = t.Split(';');
                    tempListToday.Add(new Task(Convert.ToInt16(arr[0]), Convert.ToInt16(arr[1]), arr[2], DateTime.Parse(arr[3]), arr[4], arr[5], Convert.ToBoolean(arr[6])));
                }

                cnn.Close();
                reader.Close();

                cnn.Open();

                reader = ReturnLastDayOfWeekAsSqlCommand().ExecuteReader();
                while (reader.Read())
                {
                    string t = "";
                    for (short i = 0; i < reader.FieldCount; i++)
                    {
                        t += reader.GetValue(i);
                        if (i != reader.FieldCount - 1)
                            t += ";";
                    }
                    string[] arr = t.Split(';');
                    tempListWeek.Add(new Task(Convert.ToInt16(arr[0]), Convert.ToInt16(arr[1]), arr[2], DateTime.Parse(arr[3]), arr[4], arr[5], Convert.ToBoolean(arr[6])));
                }

                cnn.Close();
                reader.Close();

                cnn.Open();

                reader = ReturnLastDayOfMonthAsSqlCommand().ExecuteReader();
                while (reader.Read())
                {
                    string t = "";
                    for (short i = 0; i < reader.FieldCount; i++)
                    {
                        t += reader.GetValue(i);
                        if (i != reader.FieldCount - 1)
                            t += ";";
                    }
                    string[] arr = t.Split(';');
                    tempListMonth.Add(new Task(Convert.ToInt16(arr[0]), Convert.ToInt16(arr[1]), arr[2], DateTime.Parse(arr[3]), arr[4], arr[5], Convert.ToBoolean(arr[6])));
                }

                cnn.Close();
                reader.Close();


                Dispatcher.Invoke(new Action(() => {
                    AllTasksToday = new ObservableCollection<Task>(tempListToday);
                    AllTasksWeek = new ObservableCollection<Task>(tempListWeek);
                    AllTasksMonth = new ObservableCollection<Task>(tempListMonth);
                }));
            }
            catch (Exception ex)
            {

            }
        }

        private SqlCommand ReturnTodayAsSqlCommand()
        {
            SqlCommand command = cnn.CreateCommand();
            DateTime today = DateTime.Today;
            string temp = today.ToString("yyyy-MM-dd");

            command.CommandText = "select * from tasks where duedate = '" + temp + "' and assignees like '%" + MainWindow.account.id + "%' and done = 0;";
            return command;
        }

        private SqlCommand ReturnLastDayOfWeekAsSqlCommand()
        {
            SqlCommand command = cnn.CreateCommand();
            DateTime lastDay = lastDayOfWeek().AddDays(1);
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string lastDayAsString = lastDay.ToString("yyyy-MM-dd");

            command.CommandText = "select * from tasks where duedate >= '" + today + "' and duedate <'" + lastDayAsString + "' and assignees like '%" + MainWindow.account.id + "%' and done = 0;";
            //+1 da man so alle bis zum letzten Tag 24:00 erhält, wie von Ehrenmüller gesagt
            return command;
        }

        private SqlCommand ReturnLastDayOfMonthAsSqlCommand()
        {
            SqlCommand command = cnn.CreateCommand();
            DateTime date = DateTime.Today;
            //string dateAsString = date.ToString("yyyy-MM-dd");
            int lastDay = DateTime.DaysInMonth(date.Year, date.Month);
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string month = date.Month + "";
            if (date.Month < 10)
                month = "0" + date.Month;
            string lastDayAsString = date.Year + "-" + month + "-" + lastDay;

            command.CommandText = "select * from tasks where duedate >= '" + today + "' and duedate <= '" + lastDayAsString + "' and assignees like '%" + MainWindow.account.id + "%' and done = 0;";
            //+1 da man so alle bis zum letzten Tag 24:00 erhält, wie von Ehrenmüller gesagt
            return command;
        }

        private DateTime lastDayOfWeek()
        {
            DateTime dt = DateTime.Today;
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            dt = dt.AddDays(-diff).Date;
            dt = dt.AddDays(6);
            return dt;
        }

        private void Button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            if(! worker.IsBusy)
                worker.RunWorkerAsync();
        }

        private void CheckBox_Click_Month(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dgrid_month.SelectedIndex;      //index of AllTasksMonth
            currentList = "month";

            worker_statechanged.RunWorkerAsync("" + selectedIndex);
        }

        private void CheckBox_Click_Week(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dgrid_week.SelectedIndex;
            currentList = "week";

            worker_statechanged.RunWorkerAsync("" + selectedIndex);
        }

        private void CheckBox_Click_Today(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dgrid_today.SelectedIndex;
            currentList = "today";

            worker_statechanged.RunWorkerAsync("" + selectedIndex);
        }

        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lv_inbox.SelectedIndex != -1)
            {
                int index = lv_inbox.SelectedIndex;
                string temp = lv_inbox.SelectedItems[0].ToString();
                tempIndex = index;
                addEntry = false;

                if (temp.Contains("Invitation"))
                {
                    MetroDialogSettings metroDialogSettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "ACCEPT",
                        NegativeButtonText = "REJECT",
                    };
                    var accepted = await MainWindow.instance.ShowMessageAsync("INVITATION",
                        "You got an invitation.", MessageDialogStyle.AffirmativeAndNegative, metroDialogSettings);

                    
                    if (accepted.ToString() == "Affirmative")
                    {
                        proj_id = AllNotifications[index].proj_id;
                        
                        historyInsert = "insert into history (proj_id, text, entry_date) values(" + proj_id + ", '[" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "]: User \"" + MainWindow.account.firstName + " " + MainWindow.account.lastName + "\" has been added to project \"";
                        addEntry = true;
                        if (!worker_invitation.IsBusy)
                            worker_invitation.RunWorkerAsync(index);
                    }
                }
                
                /*if (!worker_deleteNotification.IsBusy)
                {
                    worker_deleteNotification.RunWorkerAsync(AllNotifications[index].notification_id);

                    AllNotifications.RemoveAt(index);
                }*/
            }
        }

        private void Button_CreateProject_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            object obj = parentWindow.FindName("flyout_NewProject");
            Flyout flyout = (Flyout)obj;
            flyout.IsOpen = !flyout.IsOpen;
        }
    }
}
