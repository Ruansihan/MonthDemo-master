using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace MonthDemo
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        private MonthRecordCollection mRecords;

        public CalendarPage(MonthRecordCollection records)
        {
            mRecords = records;
            this.Loaded += new RoutedEventHandler(CalendarPage_Loaded);
            InitializeComponent();
        }

        //static Regex regex = new Regex("year=(.*)&month=(.*)");
        void CalendarPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Uri uri = this.NavigationService.Source;
            //Match match = regex.Match(uri.ToString());
            //String year = match.Groups[1].Value;
            ////String month = match.Groups[2].Value;
            //calendar1.init(Int16.Parse(year), Int16.Parse(month));
            calendar1.NotWorkdays.Clear();
            calendar1.init(mRecords.Year, mRecords.Month);
        }

        private void markAsNotWorkdaysClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in calendar1.SelectedDates)
            {
                if (!calendar1.NotWorkdays.Contains(item))
                {
                    calendar1.NotWorkdays.Add(item);
                }
            }
            calendar1.SelectedDates.Clear();
        }

        private void markAsWorkdaysClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in calendar1.SelectedDates)
            {
                if (calendar1.NotWorkdays.Contains(item))
                {
                    calendar1.NotWorkdays.Remove(item);
                }
            }
            calendar1.SelectedDates.Clear();
        }

        private void onNextStepClick(object sender, RoutedEventArgs e)
        {
            mRecords.setNotWorkdays(calendar1.NotWorkdays);
            mRecords.assemb();
            Page page = new ResultPage(mRecords);
            this.NavigationService.Navigate(page);
        }
    }
}
