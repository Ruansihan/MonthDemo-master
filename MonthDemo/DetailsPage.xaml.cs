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
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            InitializeComponent();
            var collection = MonthRecordCollection.LoadFile("G:\\WPFProject\\MonthDemo\\MonthDemo\\bin\\Debug\\errdata.txt");
            collection.setNotWorkdays(initNotWorkdays(new DateTime(2012, 10, 1), new DateTime(2012, 10, 31)));
            collection.assemb();
            var record = collection.Records[0];
            combobox.Tag = record;
            list.ItemsSource = record.Records;
        }

        private List<DateTime> initNotWorkdays(DateTime start, DateTime end)
        {
            List<DateTime> result = new List<DateTime>();
            DateTime date = start;
            while (date <= end)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday ||
                    date.DayOfWeek == DayOfWeek.Sunday)
                {
                    result.Add(date);
                }
                date = date.AddDays(1);
            }
            return result;
        } 

        public DetailsPage(MonthRecord record)
        {
            InitializeComponent();
            combobox.Tag = record;
            list.ItemsSource = record.Records;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = sender as ComboBox;
            ComboBoxItem item = combobox.SelectedValue as ComboBoxItem;
            if (item.Tag != null)
            {
                list.ItemsSource = item.Tag as List<Record>;
            }
        }
    }
}
