using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TMS.views
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : UserControl
    {
        const int MAXDAYS = 42;
        DateTime startDay;
        DateTime lastDay;
        DateTime currentMonth = DateTime.Now;
        DateTime currentYear = DateTime.Now;
        List<CalendarEntry> entrys = new List<CalendarEntry>();

        #region Database
        private string connectionString = "workstation id=tmsdatabase.mssql.somee.com;packet size=4096;" +
                "user id=mfrech_SQLLogin_1;pwd=x9v9kqzi71;data source=tmsdatabase.mssql.somee.com;" +
                "persist security info=False;initial catalog=tmsdatabase";
        private SqlConnection cnn;
        BackgroundWorker worker_getEntrys;
        #endregion

        public CalendarView()
        {
            InitializeComponent();
            cnn = new SqlConnection(connectionString);
            worker_getEntrys = new BackgroundWorker();
            worker_getEntrys.DoWork += Worker_getEntrys_DoWork;
            worker_getEntrys.RunWorkerCompleted += Worker_getEntrys_RunWorkerCompleted;

            setDays();
            setMonth(currentMonth.Month);
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(Properties.Settings.Default.Lang);
            currmonth.Content = currentMonth.ToString("MMMM", culture) + " - " + currentYear.Year;
        }

        private void Worker_getEntrys_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            setEntrys();
        }

        private void Worker_getEntrys_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand command = cnn.CreateCommand();
                command.CommandText = "select name, CONVERT(varchar, duedate, 101) from tasks where assignees like '%" + MainWindow.account.id + "%' and done = 0;";
                SqlDataReader reader;
                List<CalendarEntry> tempList = new List<CalendarEntry>();

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
                    tempList.Add(new CalendarEntry(arr[0], arr[1]));
                }

                cnn.Close();
                entrys.Clear();
                entrys = tempList;
            }
            catch (Exception ex)
            {

            }
        }

        private string getTasksAtADay(int day, int month, int year)
        {
            string names = "";
            bool first = true;

            for (int i = 0; i < entrys.Count; i++)
            {
                if(entrys[i].task_date.Year == year && entrys[i].task_date.Month == month && entrys[i].task_date.Day == day)
                {
                    if (!first)
                        names += "\n";

                    first = false;
                    names += entrys[i].task_name;
                }
            }

            return names;
        }

        private void setEntrys()
        {
            List<CalendarEntry> tempList = new List<CalendarEntry>();
            for (int i = 0; i < entrys.Count; i++)
            {
                if (entrys[i].task_date.Year == currentYear.Year && entrys[i].task_date.Month == currentMonth.Month)
                    tempList.Add(entrys[i]);
            }
            bool[] blocked = new bool[31];
            for (int a = 0; a < 31; a++)
            {
                blocked[a] = false;
            }

            for (int j = 0; j < tempList.Count; j++)
            {
                int dayOfEntry = tempList[j].task_date.Day;

                startDay = new DateTime(currentYear.Year, currentMonth.Month, 1);
                lastDay = startDay.AddMonths(1).AddDays(-1);
                int start = (int)startDay.DayOfWeek - 1;
                if (start == -1)
                    start = 6;
                int end = (int)lastDay.Day + start;
                int endOfNext = end;
                if (start == 0)
                    endOfNext += 7;
                int lastDayOfLastMonth = (int)startDay.AddDays(-1).Day;
                int count = 1;
                int row = 1;
                int pos = 0;
                int offset = start - 1;
                if (start % 7 == 0)
                {
                    offset = 6;
                }

                int tempday = 1;
                for (int i = start; i < end; i++)
                {
                    if (i % 7 == 0)
                    {
                        row++;
                        pos = 0;
                    }
                    if (tempday == dayOfEntry && blocked[tempday-1] == false)
                    {
                        Ellipse elips = new Ellipse
                        {
                            Width = 10,
                            Height = 10,
                            VerticalAlignment = VerticalAlignment.Bottom,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            Margin = new Thickness(7),
                            Stroke = Brushes.DarkGray
                        };
                        int tempPos = pos;
                        if (row == 1)
                            tempPos++;
                        Grid.SetColumn(elips, tempPos);
                        Grid.SetRow(elips, row);
                        grid.Children.Add(elips);
                        Rectangle rect = new Rectangle
                        {
                            VerticalAlignment = VerticalAlignment.Stretch,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            ToolTip = getTasksAtADay(tempList[j].task_date.Day, tempList[j].task_date.Month, tempList[j].task_date.Year),
                            Fill = Brushes.Bisque,
                            Opacity = .0
                        };
                        Grid.SetColumn(rect, tempPos);
                        Grid.SetRow(rect, row);
                        grid.Children.Add(rect);
                        blocked[tempday-1] = true;
                    }
                    tempday++;
                    pos++;
                }
            }
        }

        private void setDays()
        {
            for (int i = 0; i < 7; i++)
            {
                string day = "";
                switch (i)
                {
                    case 0:
                        day = (string)FindResource("monday");
                        break;
                    case 1:
                        day = (string)FindResource("tuesday");
                        break;
                    case 2:
                        day = (string)FindResource("thursday");
                        break;
                    case 3:
                        day = (string)FindResource("wednesday");
                        break;
                    case 4:
                        day = (string)FindResource("friday");
                        break;
                    case 5:
                        day = (string)FindResource("saturday");
                        break;
                    case 6:
                        day = (string)FindResource("sunday");
                        break;
                }
                Label temp = new Label
                {
                    Content = day,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontWeight = FontWeights.Bold
                };
                Grid.SetColumn(temp, i);
                Grid.SetRow(temp, 0);

                grid.Children.Add(temp);
            }
        }

        private void setMonth(int month)
        {
            startDay = new DateTime(currentYear.Year, month, 1);
            lastDay = startDay.AddMonths(1).AddDays(-1);
            int start = (int)startDay.DayOfWeek - 1;
            if (start == -1)
                start = 6;
            int end = (int)lastDay.Day + start;
            int endOfNext = end;
            if (start == 0)
                endOfNext += 7;
            int lastDayOfLastMonth = (int)startDay.AddDays(-1).Day;
            int count = 1;
            int row = 1;
            int pos = 0;
            int offset = start - 1;
            if (start % 7 == 0)
            {
                offset = 6;
            }

            for (int i = offset; i >= 0; i--)
            {
                Label temp = new Label
                {
                    Content = lastDayOfLastMonth,
                    Foreground = Brushes.Gray,
                    FontSize = 22
                };
                Grid.SetColumn(temp, i);
                Grid.SetRow(temp, row);

                grid.Children.Add(temp);

                pos++;
                lastDayOfLastMonth--;
            }
            for (int i = start; i < end; i++)
            {
                if (i % 7 == 0)
                {
                    row++;
                    pos = 0;
                }
                Label temp = new Label
                {
                    Content = count,
                    FontSize = 22
                };
                Grid.SetColumn(temp, pos);
                Grid.SetRow(temp, row);

                grid.Children.Add(temp);
                if (currentMonth.Month == DateTime.Now.Month && currentYear.Year == DateTime.Now.Year && count == (int)DateTime.Now.Day)
                {
                    Rectangle border = new Rectangle
                    {
                        Stroke = Brushes.DarkTurquoise,
                        StrokeThickness = 1,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        Fill = Brushes.SteelBlue,
                        Margin = new Thickness(0, 0, 0.5, 0.5),
                        Opacity = .25
                    };

                    Grid.SetColumn(border, pos);
                    Grid.SetRow(border, row);

                    grid.Children.Add(border);
                }
                count++;
                pos++;
            }
            count = 1;
            for (int i = endOfNext; i < MAXDAYS; i++)
            {
                if (i % 7 == 0)
                {
                    row++;
                    pos = 0;
                }
                Label temp = new Label
                {
                    Content = count,
                    Foreground = Brushes.Gray,
                    FontSize = 22
                };
                Grid.SetColumn(temp, pos);
                Grid.SetRow(temp, row);

                grid.Children.Add(temp);
                count++;
                pos++;
            }
        }

        private void ReloadGrid()
        {
            int collection = grid.Children.Count;
            for (int i = collection - 1; i >= 0; i--)
            {
                if (grid.Children[i].GetType().Name.ToString() == "Label" ||
                    grid.Children[i].GetType().Name.ToString() == "Rectangle" ||
                    grid.Children[i].GetType().Name.ToString() == "Ellipse")
                {
                    grid.Children.Remove(grid.Children[i] as UIElement);
                }
            }

            InitializeComponent();
        }

        private void btn_prevMonth_Click(object sender, RoutedEventArgs e)
        {
            ReloadGrid();
            int tempmonth = currentMonth.Month;
            currentMonth = currentMonth.AddMonths(-1);
            if (currentMonth.Month == 12 && tempmonth == 1)
            {
                currentYear = currentYear.AddYears(-1);
            }
            setMonth(currentMonth.Month);
            setDays();
            setEntrys();

            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(Properties.Settings.Default.Lang);
            currmonth.Content = currentMonth.ToString("MMMM", culture) + " - " + currentYear.Year;
        }

        private void btn_nextMonth_Click(object sender, RoutedEventArgs e)
        {
            ReloadGrid();
            int tempmonth = currentMonth.Month;
            currentMonth = currentMonth.AddMonths(1);
            if (currentMonth.Month == 1 && tempmonth == 12)
            {
                currentYear = currentYear.AddYears(1);
            }
            setMonth(currentMonth.Month);
            setDays();
            setEntrys();

            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(Properties.Settings.Default.Lang);
            currmonth.Content = currentMonth.ToString("MMMM", culture) + " - " + currentYear.Year;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!worker_getEntrys.IsBusy)
                worker_getEntrys.RunWorkerAsync();
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            ReloadGrid();
            setMonth(currentMonth.Month);
            setDays();

            if (!worker_getEntrys.IsBusy)
                worker_getEntrys.RunWorkerAsync();
        }
    }
}
