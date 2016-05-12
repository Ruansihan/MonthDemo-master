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

namespace MonthDemo
{
    /// <summary>
    /// Interaction logic for ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {
        private MonthRecordCollection mRecords;

        public ResultPage()
        {
            mRecords = MonthRecordCollection.LoadFile("errdata.txt");
            InitializeComponent();
            expectedWorkHours.DataContext = mRecords.ExpectedWorkHours;
            recordList.ItemsSource = mRecords.Records;
        }

        public ResultPage(MonthRecordCollection records)
        {
            mRecords = records;
            InitializeComponent();
            expectedWorkHours.DataContext = mRecords.ExpectedWorkHours;
            recordList.ItemsSource = mRecords.Records;
        }

        private void onDetailsClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MonthRecord record = button.DataContext as MonthRecord;
            var page = new DetailsPage(record);
            this.NavigationService.Navigate(page);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
