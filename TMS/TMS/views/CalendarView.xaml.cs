using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;
using System.Windows.Shapes;

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

        public CalendarView()
        {
            InitializeComponent();

            setDays();
            setMonth(currentMonth.Month);
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            currmonth.Content = currentMonth.ToString("MMMM", culture) + " - " + currentYear.Year;
        }

        private void setDays()
        {
            for (int i = 0; i < 7; i++)
            {
                string day = "";
                switch (i)
                {
                    case 0:
                        day = DayOfWeek.Monday.ToString();
                        break;
                    case 1:
                        day = DayOfWeek.Tuesday.ToString();
                        break;
                    case 2:
                        day = DayOfWeek.Wednesday.ToString();
                        break;
                    case 3:
                        day = DayOfWeek.Thursday.ToString();
                        break;
                    case 4:
                        day = DayOfWeek.Friday.ToString();
                        break;
                    case 5:
                        day = DayOfWeek.Saturday.ToString();
                        break;
                    case 6:
                        day = DayOfWeek.Sunday.ToString();
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
            Ellipse elips = new Ellipse
            {
                Width = 10,
                Height = 10,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(7),
                Stroke = Brushes.DarkGray
            };
            Grid.SetColumn(elips, 3);
            Grid.SetRow(elips, 3);
            grid.Children.Add(elips);
            Rectangle rect = new Rectangle
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                ToolTip = "Task1\nTask2",
                Fill = Brushes.Bisque,
                Opacity = .0
            };
            Grid.SetColumn(rect, 3);
            Grid.SetRow(rect, 3);
            grid.Children.Add(rect);

            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
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

            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            currmonth.Content = currentMonth.ToString("MMMM", culture) + " - " + currentYear.Year;
        }
    }
}
