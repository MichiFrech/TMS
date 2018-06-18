using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Text;

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
        public List<Project> allProjects = new List<Project>();
        public ObservableCollection<Task> filteredTasks { get; set; }
        public List<string> stringProj = new List<string>();
        public ObservableCollection<Account> nameList = new ObservableCollection<Account>();
        private List<Task> filteredTaskList;
        private List<Task> tempList = new List<Task>();
        private List<string> historyEntrys = new List<string>();
        private int currentid = 0;
        private int currentTaskID = 0;
        private string proj_ids = "";
        private int DelIndex = 0;
        private string commandTextNotification = "";
        private string historyInsertString = "";
        private string notificationString = "";
        private string notificationRemoveString = "";
        private string historyStateEntry = "";
        private Task tempTask = null;
        private bool removeAssign = false;
        private bool addedAssign = false;

        #region Database
        private string connectionString = "workstation id=tmsdatabase.mssql.somee.com;packet size=4096;" +
                "user id=mfrech_SQLLogin_1;pwd=x9v9kqzi71;data source=tmsdatabase.mssql.somee.com;" +
                "persist security info=False;initial catalog=tmsdatabase";
        private SqlConnection cnn;
        BackgroundWorker worker;
        BackgroundWorker worker_tasks;
        BackgroundWorker worker_insertTask;
        BackgroundWorker worker_statechanged;
        BackgroundWorker worker_savetask;
        BackgroundWorker worker_delete;
        BackgroundWorker worker_sendnotification;
        BackgroundWorker worker_sendnotificationRemove;
        BackgroundWorker worker_AddToHistory;
        BackgroundWorker worker_AddStateToHistory;
        BackgroundWorker worker_getHistory;
        #endregion


        public ProjectView()
        {
            InitializeComponent();
            cnn = new SqlConnection(connectionString);

            filteredTasks = new ObservableCollection<Task>();
            filteredTaskList = new List<Task>();

            InitializeAllWorkers();
        }

        private void InitializeAllWorkers()
        {
            worker_tasks = new BackgroundWorker();
            worker_tasks.DoWork += Worker_tasks_DoWork;
            worker_tasks.RunWorkerCompleted += Worker_tasks_RunWorkerCompleted;
            worker_tasks.WorkerSupportsCancellation = true;

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker_insertTask = new BackgroundWorker();
            worker_insertTask.DoWork += Worker_insertTask_DoWork;
            worker_insertTask.RunWorkerCompleted += Worker_insertTask_RunWorkerCompleted;

            worker_statechanged = new BackgroundWorker();
            worker_statechanged.DoWork += Worker_statechanged_DoWork;
            worker_statechanged.RunWorkerCompleted += Worker_statechanged_RunWorkerCompleted;

            worker_savetask = new BackgroundWorker();
            worker_savetask.DoWork += Worker_savetask_DoWork;
            worker_savetask.RunWorkerCompleted += Worker_savetask_RunWorkerCompleted;

            worker_delete = new BackgroundWorker();
            worker_delete.DoWork += Worker_delete_DoWork;
            worker_delete.RunWorkerCompleted += Worker_delete_RunWorkerCompleted;

            worker_sendnotification = new BackgroundWorker();
            worker_sendnotification.DoWork += Worker_sendnotification_DoWork;
            worker_sendnotification.RunWorkerCompleted += Worker_sendnotification_RunWorkerCompleted;

            worker_sendnotificationRemove = new BackgroundWorker();
            worker_sendnotificationRemove.DoWork += Worker_sendnotificationRemove_DoWork;
            worker_sendnotificationRemove.RunWorkerCompleted += Worker_sendnotificationRemove_RunWorkerCompleted;

            worker_AddToHistory = new BackgroundWorker();
            worker_AddToHistory.DoWork += Worker_AddToHistory_DoWork;
            worker_AddToHistory.RunWorkerCompleted += Worker_AddToHistory_RunWorkerCompleted;

            worker_AddStateToHistory = new BackgroundWorker();
            worker_AddStateToHistory.DoWork += Worker_AddStateToHistory_DoWork;
            worker_AddStateToHistory.RunWorkerCompleted += Worker_AddStateToHistory_RunWorkerCompleted;

            worker_getHistory = new BackgroundWorker();
            worker_getHistory.DoWork += Worker_getHistory_DoWork;
            worker_getHistory.RunWorkerCompleted += Worker_getHistory_RunWorkerCompleted;
        }

        private void Worker_AddStateToHistory_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!worker_sendnotification.IsBusy && commandTextNotification != "")
                worker_sendnotification.RunWorkerAsync(commandTextNotification);
        }

        private void Worker_AddStateToHistory_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();

                command.CommandText = e.Argument.ToString();

                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_sendnotificationRemove_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!worker_sendnotification.IsBusy && addedAssign)
                worker_sendnotification.RunWorkerAsync(notificationString);

            addedAssign = false;
        }

        private void Worker_savetask_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!worker_sendnotificationRemove.IsBusy && removeAssign)
                worker_sendnotificationRemove.RunWorkerAsync(notificationRemoveString);
            else {
                if (!worker_sendnotification.IsBusy && addedAssign) {
                    worker_sendnotification.RunWorkerAsync(notificationString);
                }

                addedAssign = false;
            }

            removeAssign = false;
        }

        private void Worker_getHistory_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsEnabledAllButtons(true);
        }

        private void Worker_AddToHistory_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!worker_sendnotification.IsBusy)
                worker_sendnotification.RunWorkerAsync(notificationString);
            else
                IsEnabledAllButtons(true);
        }

        private void Worker_insertTask_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!worker_AddToHistory.IsBusy)
                worker_AddToHistory.RunWorkerAsync(historyInsertString);
            else
                IsEnabledAllButtons(true);

            if (tempTask != null) {
                AllTasks.Add(tempTask);
                tempList.Add(tempTask);
            }
            for (int i = 0; i < AllTasks.Count; i++)
            {
                AllTasks[i].Assignees = GetAssignees(AllTasks[i].Assignees);
            }
        }

        private void ReloadHistory()
        {
            if (!worker_getHistory.IsBusy)
                worker_getHistory.RunWorkerAsync(allProjects[dropdown_allprojects.SelectedIndex].proj_id);
            else {
                Debug.WriteLine("worker_history is busy!");
                IsEnabledAllButtons(true);
            }
        }

        private void Worker_getHistory_DoWork(object sender, DoWorkEventArgs e)
        {   //select text from history where proj_id = 1;
            try
            {
                SqlCommand command = cnn.CreateCommand();
                int id = currentid;
                command.CommandText = "select text from history where proj_id = " + e.Argument + " order by entry_date asc;";
                SqlDataReader reader;
                List<string> tempList = new List<string>();

                //if(cnn.State == System.Data.ConnectionState.Closed)
                cnn.Open();
                
                reader = command.ExecuteReader();
                while (reader.Read()) {
                    tempList.Add(reader.GetValue(0).ToString());
                }

                reader.Close();
                cnn.Close();

                historyEntrys = tempList;
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_AddToHistory_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();

                command.CommandText = e.Argument.ToString();

                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_sendnotificationRemove_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();

                command.CommandText = e.Argument.ToString();

                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_statechanged_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!worker_AddStateToHistory.IsBusy)
                worker_AddStateToHistory.RunWorkerAsync(historyStateEntry);
        }

        private void Worker_sendnotification_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            commandTextNotification = "";
            IsEnabledAllButtons(true);
        }

        private void Worker_delete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AllTasks.RemoveAt(DelIndex);
            settingsView._currentIndex = -1;
            btn_del.IsEnabled = true;
        }

        private void Worker_sendnotification_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();

                command.CommandText = e.Argument.ToString();

                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_delete_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();

                command.CommandText = "delete from tasks where task_id = " + e.Argument + ";";

                if(cnn.State == System.Data.ConnectionState.Closed)
                    cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_savetask_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();

                command.CommandText = "update tasks set " + e.Argument + " where task_id = " + currentTaskID + ";";

                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_statechanged_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();
                int doneVal = Convert.ToInt16(AllTasks[Convert.ToInt16(e.Argument)].Done);
                int taskID = AllTasks[Convert.ToInt16(e.Argument)].Task_id;

                command.CommandText = "update tasks set done = " + doneVal + " where task_id = " + taskID + ";";

                if(cnn.State == System.Data.ConnectionState.Closed)
                    cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();

                Task tempTask = AllTasks[Convert.ToInt16(e.Argument)];
                string tempAssignees = tempTask.AssigneesWithoutName;
                int tempProjID = tempTask.Proj_id;
                string message = "Task " + ((char)34) + tempTask.Name + ((char)34);
                if (doneVal == 1)
                    commandTextNotification = "insert into notifications (acc_id, message) values('" + tempAssignees + "', '" + message + " is done');";
                else
                    commandTextNotification = "insert into notifications (acc_id, message) values('" + tempAssignees + "', '" + message + " is still to be done until " + tempTask.Deadline + "');";
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_insertTask_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();
                command.CommandText = "insert into tasks (proj_id, name, duedate, assignees, description) values(" + e.Argument + ");";

                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private string GetAssignees(string assignees)
        {
            string temp = "";
            bool first = true;
            for (int i = 0; i < nameList.Count; i++)
            {
                if (assignees.Contains(nameList[i].id.ToString()))
                {
                    if (!first)
                        temp += ", ";
                    temp += nameList[i].firstName + " " + nameList[i].lastName;
                    first = false;
                }
            }

            return temp;
        }

        private void Worker_tasks_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            for (int i = 0; i < AllTasks.Count; i++)
            {
                AllTasks[i].Assignees = GetAssignees(AllTasks[i].Assignees);
            }
            dgrid.ItemsSource = AllTasks;
            string t = GetAssignees(allProjects[dropdown_allprojects.SelectedIndex].AssigneesToString());
            addView = new AddView(t);
            settingsView = new SettingsView(t);
            this.gb_settings.Content = this.settingsView;
            this.gb_add.Content = this.addView;
            ReloadHistory();

            tempList.Clear();
            for (int i = 0; i < AllTasks.Count; i++)
            {
                tempList.Add(AllTasks[i]);
            }
        }

        private void Worker_tasks_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();
                int id = currentid;
                command.CommandText = "select * from tasks where proj_id = " + id + ";";
                SqlDataReader reader;
                List<Task> tempList = new List<Task>();

                if(cnn.State != System.Data.ConnectionState.Open)
                    cnn.Open();

                reader = command.ExecuteReader();
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
                    tempList.Add(new Task(Convert.ToInt16(arr[0]), Convert.ToInt16(arr[1]), arr[2], DateTime.Parse(arr[3]), arr[4], arr[5], Convert.ToBoolean(arr[6])));
                }

                reader.Close();
                List<Account> AccList = new List<Account>();
                command.CommandText = "select acc_id, firstname, lastname from accounts where acc_id in(" + proj_ids + ");";
                
                reader = command.ExecuteReader();
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
                    AccList.Add(new Account(Convert.ToInt16(arr[0]), arr[1], arr[2]));
                }
                cnn.Close();
                Dispatcher.Invoke(new Action(() => {
                    AllTasks = new ObservableCollection<Task>(tempList);
                    nameList = new ObservableCollection<Account>(AccList);
                }));
            }
            catch (Exception ex)
            {

            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool first = true;
            List<string> listOfProj = new List<string>();
            for (int i = 0; i < allProjects.Count; i++)
            {
                listOfProj.Add(allProjects[i].proj_name);
                List<string> te = allProjects[i].proj_assignees;
                for (int a = 0; a < te.Count; a++) {
                    if (!proj_ids.Contains(te[a])) {
                        if (!first) {
                            proj_ids += ",";
                        }
                        proj_ids += te[a];
                        first = false;
                    }
                }
                
            }

            stringProj = listOfProj;
            if(listOfProj.Count != 0)
            {
                dropdown_allprojects.ItemsSource = stringProj;
                dropdown_allprojects.SelectedIndex = 0;
                proj_due.Content = (string)FindResource("deadline_task") + "   " + allProjects[dropdown_allprojects.SelectedIndex].proj_duedate.ToString("dd-MM-yyyy");
                btn_history.IsEnabled = true;
                btn_invite.IsEnabled = true;
                btn_add.IsEnabled = true;
                btn_save.IsEnabled = true;
                btn_del.IsEnabled = true;
                btn_filter.IsEnabled = true;
                btn_refresh.IsEnabled = true;
            }

            if (allProjects[dropdown_allprojects.SelectedIndex].proj_owner == MainWindow.account.id)
                btn_history.IsEnabled = true;
            else
                btn_history.IsEnabled = false;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();
                command.CommandText = "select * from projects where assignees like '%" + MainWindow.account.id + "%';";
                SqlDataReader reader;
                List<Project> tempList = new List<Project>();

                cnn.Open();

                reader = command.ExecuteReader();
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
                    tempList.Add(new Project(Convert.ToInt16(arr[0]), arr[1], arr[2], arr[3], Convert.ToInt16(arr[4])));
                }

                cnn.Close();
                Dispatcher.Invoke(new Action(() => {
                    allProjects = tempList;
                }));
            }
            catch (Exception ex)
            {

            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool insertable = true;
            string insertString = allProjects[dropdown_allprojects.SelectedIndex].proj_id.ToString() + ", '";
            string taskString = "-1;" + allProjects[dropdown_allprojects.SelectedIndex].proj_id.ToString() + ";";

            if (addView.Name != null && addView.Name.Length <= 50) {
                insertString += addView.Name + "', ";
                taskString += addView.Name + ";";
            }
            else
                insertable = false;
            if (addView.Deadline != null)
            {
                insertString += "CONVERT (date, '" + addView.Deadline.Value.ToString("yyyy-MM-dd") + "'), '";
                taskString += addView.Deadline.Value.ToString("yyyy-MM-dd") + ";";
            }
            else
            {
                insertString += "CONVERT (date, '2099-12-31'), '";
                taskString += "2099-12-31;";
            }
            List<int> tempID = new List<int>();
            if (addView.listView.SelectedItems.Count != 0)
            {
                for (int i = 0; i < addView.listView.SelectedItems.Count; i++)
                {
                    for (int a = 0; a < nameList.Count; a++)
                    {
                        if (addView.listView.SelectedItems[i].ToString() == nameList[a].firstName + " " + nameList[a].lastName)
                        {
                            tempID.Add(nameList[a].id);
                            break;
                        }
                    }
                }

                bool first = true;
                for (int j = 0; j < tempID.Count; j++)
                {
                    if (!first) {
                        insertString += ",";
                        taskString += ",";
                    }
                    insertString += tempID[j];
                    taskString += tempID[j];

                    first = false;
                }
                insertString += "', ";
            }
            else
                insertable = false;
            insertString += "null";

            addView.ClearAllControls();

            if (insertable) {
                string[] arr = taskString.Split(';');
                tempTask = new Task(Convert.ToInt16(arr[0]), Convert.ToInt16(arr[1]), arr[2], DateTime.ParseExact(arr[3], "yyyy-MM-dd", null), arr[4], null, false);
                
                notificationString = "insert into notifications (acc_id, message) values('" + arr[4] + "', 'You have been added to task \"" + arr[2] + "\" for project \"" + allProjects[dropdown_allprojects.SelectedIndex].proj_name + "\"');";
                historyInsertString = "insert into history (proj_id, text, entry_date) values(" + allProjects[dropdown_allprojects.SelectedIndex].proj_id + ", '[" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "]: Task \"" + arr[2] + "\" has been created in project \"" + allProjects[dropdown_allprojects.SelectedIndex].proj_name + "\" by user \"" + MainWindow.account.firstName + " " + MainWindow.account.lastName + "\"', CONVERT (date, '" + DateTime.Now.ToString("yyyy-MM-dd") + "'));";
                IsEnabledAllButtons(false);
                if (!worker_insertTask.IsBusy)
                    worker_insertTask.RunWorkerAsync(insertString);
                else {
                    Debug.WriteLine("worker_insertTask is busy!");
                    IsEnabledAllButtons(true);
                }
                
                lbl_error.Visibility = Visibility.Hidden;
            } else {
                lbl_error.Visibility = Visibility.Visible;
            }
        }

        private void dgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgrid.SelectedIndex != -1) {
                this.settingsView.SelectionChanged(dgrid.SelectedIndex);

                settingsView.SetNewTask(AllTasks[settingsView._currentIndex]);
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (settingsView._currentIndex != -1 && dgrid.SelectedIndex != -1)
            {
                btn_del.IsEnabled = false;
                DelIndex = dgrid.SelectedIndex;
                if (!worker_delete.IsBusy)
                    worker_delete.RunWorkerAsync(AllTasks[dgrid.SelectedIndex].Task_id);
            }
        }

        private void Button_History_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            object obj = parentWindow.FindName("flyout");
            Flyout flyout = (Flyout)obj;
            object lbl = parentWindow.FindName("lbl_proj");
            Label projlbl = (Label)lbl;
            projlbl.Content = (string)FindResource("history_of") + " " + this.dropdown_allprojects.SelectedItem.ToString();
            object lv = parentWindow.FindName("listview");
            ListView listView = (ListView)lv;
            listView.Items.Clear();
            for (int i = 0; i < historyEntrys.Count; i++) {
                listView.Items.Add(historyEntrys[i]);
            }

            flyout.IsOpen = !flyout.IsOpen;
        }

        private async void Button_UserAdd_Click(object sender, RoutedEventArgs e)
        {
            if(allProjects[dropdown_allprojects.SelectedIndex].proj_owner == MainWindow.account.id)
            {
                var value = await MainWindow.instance.ShowInputAsync((string)FindResource("invite_headline"), (string)FindResource("invite_email"));

                if (value != null && value != "")
                {
                    string currentProjectName = allProjects[dropdown_allprojects.SelectedIndex].proj_name;
                    string invitationText = "Invitation to " + ((char)34);
                    invitationText += currentProjectName;
                    invitationText += ((char)34);
                    string email = value;
                    int projectID = allProjects[dropdown_allprojects.SelectedIndex].proj_id;
                    notificationString = "insert into notifications values((select acc_id from accounts where email = '" + email + "'), '" + invitationText + "', " + projectID + ");";

                    if (!worker_sendnotification.IsBusy)
                        worker_sendnotification.RunWorkerAsync(notificationString);
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(!worker.IsBusy)
                worker.RunWorkerAsync();
        }

        private void dropdown_allprojects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dropdown_allprojects.SelectedIndex == -1)
            {
                currentid = allProjects[0].proj_id;
                proj_due.Content = "Deadline:   " + allProjects[0].proj_duedate.ToString("dd-MM-yyyy");
            }
            else
            {
                currentid = allProjects[dropdown_allprojects.SelectedIndex].proj_id;
                proj_due.Content = "Deadline:   " + allProjects[dropdown_allprojects.SelectedIndex].proj_duedate.ToString("dd-MM-yyyy");
            }

            if(! worker_tasks.IsBusy)
                worker_tasks.RunWorkerAsync();

            if (allProjects[dropdown_allprojects.SelectedIndex].proj_owner == MainWindow.account.id)
                btn_history.IsEnabled = true;
            else
                btn_history.IsEnabled = false;

            Window parentWindow = Window.GetWindow(this);
            object obj = parentWindow.FindName("flyout_filterTasks");
            Flyout flyout = (Flyout)obj;
            flyout.IsOpen = false;
        }

        private void CheckBox_Done_Click(object sender, RoutedEventArgs e)
        {
            string state = AllTasks[dgrid.SelectedIndex].Done ? "closed" : "opened";
            historyStateEntry = "insert into history (proj_id, text, entry_date) values(" + allProjects[dropdown_allprojects.SelectedIndex].proj_id + ", '[" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "]: Task \"" + AllTasks[dgrid.SelectedIndex].Name + "\" has been " + state + " in project \"" + allProjects[dropdown_allprojects.SelectedIndex].proj_name + "\" by user \"" + MainWindow.account.firstName + " " + MainWindow.account.lastName + "\"', CONVERT (date, '" + DateTime.Now.ToString("yyyy-MM-dd") + "'));";
            if (! worker_statechanged.IsBusy)
                worker_statechanged.RunWorkerAsync(dgrid.SelectedIndex.ToString());
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(dgrid.Items.Count > 0 && dgrid.SelectedIndex != -1)
            {
                bool insertable = true;
                string insertString = "";
                currentTaskID = AllTasks[dgrid.SelectedIndex].Task_id;
                string taskString = "-1;" + allProjects[dropdown_allprojects.SelectedIndex].proj_id.ToString() + ";";

                if (settingsView.Name != null && settingsView.Name != "")
                {
                    insertString += "name = '" + settingsView.Name + "', ";
                    taskString += settingsView.Name + ";";
                }
                else
                    insertable = false;
                if (settingsView.Deadline != null)
                {
                    insertString += "duedate = CONVERT (date, '" + settingsView.Deadline.Value.ToString("yyyy-MM-dd") + "'), assignees = '";
                    taskString += settingsView.Deadline.Value.ToString("yyyy-MM-dd") + ";";
                }
                else
                {
                    insertString += "duedate = CONVERT (date, '2099-12-31'), assignees = '";
                    taskString += "2099-12-31;";
                }
                List<int> tempID = new List<int>();
                if (settingsView.listView.SelectedItems != null)
                {
                    for (int i = 0; i < settingsView.listView.SelectedItems.Count; i++)
                    {
                        for (int a = 0; a < nameList.Count; a++)
                        {
                            if (settingsView.listView.SelectedItems[i].ToString() == nameList[a].firstName + " " + nameList[a].lastName)
                            {
                                tempID.Add(nameList[a].id);
                                break;
                            }
                        }
                    }

                    bool first = true;
                    for (int j = 0; j < tempID.Count; j++)
                    {
                        if (!first)
                        {
                            insertString += ",";
                            taskString += ",";
                        }
                        insertString += tempID[j];
                        taskString += tempID[j];

                        first = false;
                    }
                    insertString += "', ";
                    taskString += ";";
                }
                else
                    insertable = false;
                if (settingsView.Desc != null)
                {
                    if (settingsView.Desc == "")
                    {
                        insertString += "description = null";
                        taskString += "null";
                    }
                    else
                    {
                        insertString += "description = '" + settingsView.Desc + "'";
                        taskString += settingsView.Desc;
                    }
                }
                else
                {
                    insertable = false;
                }

                if (insertable)
                {
                    string[] arr = taskString.Split(';');
                    int index = dgrid.SelectedIndex;
                    dgrid.SelectedIndex = -1;
                    bool doneVal = AllTasks[index].Done;
                    string tempAssigness = AllTasks[index].AssigneesWithoutName;
                    AllTasks.RemoveAt(index);
                    AllTasks.Insert(index, new Task(Convert.ToInt16(arr[0]), Convert.ToInt16(arr[1]), arr[2], DateTime.ParseExact(arr[3], "yyyy-MM-dd", null), arr[4], arr[5], doneVal));
                    AllTasks[index].Assignees = GetAssignees(AllTasks[index].Assignees);
                    dgrid.SelectedIndex = index;

                    string assign = arr[4];
                    string newAssign = "";
                    string remAssign = "";
                    bool first = true;
                    List<string> tempList = arr[4].Split(',').ToList();
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (!tempAssigness.Contains(tempList[i]))
                        {
                            if (!first)
                                newAssign += ",";
                            newAssign += tempList[i];
                            first = false;
                        }
                    }
                    tempList = tempAssigness.Split(',').ToList();
                    first = true;
                    for (int a = 0; a < tempList.Count; a++)
                    {
                        if (!arr[4].Contains(tempList[a]))
                        {
                            if (!first)
                                remAssign += ",";
                            remAssign += tempList[a];
                            first = false;
                        }
                    }

                    notificationString = "insert into notifications (acc_id, message) values('" + newAssign + "', 'You have been added to task \"" + arr[2] + "\" for project \"" + allProjects[dropdown_allprojects.SelectedIndex].proj_name + "\"');";
                    notificationRemoveString = "insert into notifications (acc_id, message) values('" + remAssign + "', 'You have been removed from task \"" + arr[2] + "\" at project \"" + allProjects[dropdown_allprojects.SelectedIndex].proj_name + "\"');";

                    if (!worker_savetask.IsBusy)
                        worker_savetask.RunWorkerAsync(insertString);

                    if (remAssign != "")
                        removeAssign = true;

                    if (newAssign != "")
                        addedAssign = true;
                    lbl_error.Visibility = Visibility.Hidden;
                }
                else
                    lbl_error.Visibility = Visibility.Visible;
            }
        }

        private void Button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            if(! worker_tasks.IsBusy)
                worker_tasks.RunWorkerAsync();
        }

        private void IsEnabledAllButtons(bool isEnabled)
        {
            btn_filter.IsEnabled = isEnabled;
            btn_refresh.IsEnabled = isEnabled;
            btn_add.IsEnabled = isEnabled;
            btn_save.IsEnabled = isEnabled;
            btn_del.IsEnabled = isEnabled;
        }

        private void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            object obj = parentWindow.FindName("flyout_filterTasks");
            Flyout flyout = (Flyout)obj;
            flyout.IsOpen = !flyout.IsOpen;
            object lv = parentWindow.FindName("lvFilterByAsignees");
            ListView listViewForAsignees = (ListView)lv;
            object btn = parentWindow.FindName("btnOkForFilterTasks");
            Button applyBtn = (Button)btn;
            applyBtn.Click += ApplyFilter_Click;

            List<string> temp = GetAssignees(allProjects[dropdown_allprojects.SelectedIndex].AssigneesToString()).Split(',').ToList();
            for (int i = 0; i < temp.Count; i++)
            {
                string tempStr = temp[i];
                temp[i] = tempStr.TrimStart(' ');
            }
            listViewForAsignees.ItemsSource = temp;
        }

        private async void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            object n = parentWindow.FindName("tbFilterByName");
            TextBox tbName = (TextBox)n;
            string name = tbName.Text;

            object from = parentWindow.FindName("dpFilterByDateFrom");
            DatePicker dpFrom = (DatePicker)from;
            string fromDate = dpFrom.Text;

            object until = parentWindow.FindName("dpFilterByDateUntil");
            DatePicker dpUntil = (DatePicker)until;
            string untilDate = dpUntil.Text;

            object asigneesList = parentWindow.FindName("lvFilterByAsignees");
            ListView lvAsignees = (ListView)asigneesList;

            List<int> selectedAsigneesIDs = new List<int>();
            int t = 0;
            for (int i = 0; i < lvAsignees.Items.Count; i++)
            {
                if (lvAsignees.SelectedItems.Count != t && lvAsignees.Items[i] == lvAsignees.SelectedItems[t])
                {
                    int id = nameList[i].id;
                    t++;
                    selectedAsigneesIDs.Add(id);
                }
            }

            if (fromDate != "" && untilDate != "")
            {
                DateTime fromDateAsDateTime = DateTime.Parse(fromDate);
                DateTime untilDateAsDateTime = DateTime.Parse(untilDate);

                if (untilDateAsDateTime < fromDateAsDateTime)
                {
                    await MainWindow.instance.ShowMessageAsync("You have set an date in the until-cell which is earlier then the date of the from-cell", "Please try again with different values.");
                }
            }

            filteredTaskList.Clear();
            decideWhatToFilter(name, fromDate, untilDate, selectedAsigneesIDs);

            object obj = parentWindow.FindName("flyout_filterTasks");
            Flyout flyout = (Flyout)obj;
            flyout.IsOpen = false;
        }

        private async void decideWhatToFilter(string name, string fromDate, string untilDate, List<int> selectedAsigneesIDs)
        {
            bool temp = false;
            bool date = true;
            bool date2 = true;
            for (int i = 0; i < tempList.Count; i++)
            {
                filteredTaskList.Add(tempList[i]);
            }

            if (name != "")
            {
                filterTasksByName(name);
                temp = true;
            }

            //Muss vor fromDate sein da z.B. eine deadline unter der untilDate sein kann aber von fromDate gelöscht wird da die deadline kleiner fromDate ist
            if (untilDate != "")
            {
                temp = true;
            }
            else
                date = false;

            if (fromDate != "")
            {
                temp = true;
            }
            else
                date2 = false;

            if (date || date2)
                filterTasksByDate(fromDate, untilDate);

            if (selectedAsigneesIDs.Count > 0)
            {
                filterTasksByAsignees(selectedAsigneesIDs);
                temp = true;
            }


            if (temp == false)
            {
                AllTasks.Clear();
                for (int i = 0; i < tempList.Count; i++)
                {
                    AllTasks.Add(tempList[i]);
                }
            }
            else
            {
                if (filteredTaskList.Count == 0)
                {
                    await MainWindow.instance.ShowMessageAsync((string)FindResource("filter_nomatch_header"), (string)FindResource("filter_nomatch"));
                }
                else
                {
                    AllTasks.Clear();
                    for (int i = 0; i < filteredTaskList.Count; i++)
                    {
                        AllTasks.Add(filteredTaskList[i]);
                    }
                }
            }
        }

        private void filterTasksByName(string name)
        {
            //Da immer eins gelöscht wird muss man rückwärts durchgehen; Ansonsten wird nur bis zur Hälfte durchgegangen wenn man normal darüber iteriert -> siehe Debugging
            for (int i = tempList.Count - 1; i > -1; i--)
            {
                if (!tempList[i].Name.Contains(name))
                {
                    filteredTaskList.Remove(tempList[i]);
                }
            }

        }

        private void filterTasksByDate(string fromDate, string untilDate)
        {
            DateTime fromDateAsDateTime;
            DateTime untilDateAsDateTime;
            if (fromDate == "")
                fromDateAsDateTime = new DateTime(1000, 1, 1);
            else
                fromDateAsDateTime = DateTime.Parse(fromDate);

            if (untilDate == "")
                untilDateAsDateTime = new DateTime(9999, 12, 31);
            else
                untilDateAsDateTime = DateTime.Parse(untilDate);
            DateTime deadlineAsDateTime;
            //Da immer eins gelöscht wird muss man rückwärts durchgehen; Ansonsten wird nur bis zur Hälfte durchgegangen wenn man normal darüber iteriert -> siehe Debugging
            for (int i = tempList.Count - 1; i > -1; i--)
            {
                deadlineAsDateTime = DateTime.ParseExact(tempList[i].Deadline, "dd-MM-yyyy", null);

                if (deadlineAsDateTime < fromDateAsDateTime || deadlineAsDateTime > untilDateAsDateTime)
                {
                    filteredTaskList.Remove(tempList[i]);
                }
            }
        }

        private void filterTasksByAsignees(List<int> selectedAsigneesIDs)
        {
            //Da immer eins gelöscht wird muss man rückwärts durchgehen; Ansonsten wird nur bis zur Hälfte durchgegangen wenn man normal darüber iteriert -> siehe Debugging
            bool containsSelected = true;

            for (int i = tempList.Count - 1; i > -1; i--)
            {
                for (int j = 0; j < selectedAsigneesIDs.Count; j++)
                {
                    if (!tempList[i].AssigneesWithoutName.Contains(selectedAsigneesIDs[j].ToString()))
                    {
                        containsSelected = false;

                    }
                }

                if (containsSelected == false)
                    filteredTaskList.Remove(tempList[i]);

                containsSelected = true;
            }
        }

        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV File|*.csv";
            sfd.ShowDialog();

            if(sfd.FileName != "")
            {
                List<string> file = new List<string>();
                file.Add("Name;Description;Members");
                for (int i = 0; i < AllTasks.Count; i++)
                {
                    
                    file.Add(AllTasks[i].Name + ";" + AllTasks[i].Deadline + ";" + AllTasks[i].Assignees);
                }

                File.WriteAllLines(Path.GetFullPath(sfd.FileName), file, Encoding.Default);
            }
        }
    }
}
