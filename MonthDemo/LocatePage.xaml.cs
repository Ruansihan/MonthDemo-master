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
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;

namespace MonthDemo
{
    /// <summary>
    /// Interaction logic for LocatePage.xaml
    /// </summary>
    public partial class LocatePage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MonthRecordCollection mRecords;
        public MonthRecordCollection Records
        {
            get
            {
                return mRecords;
            }
            private set
            {
                mRecords = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsMonthRecordValid"));
                }
            }
        }

        public bool IsMonthRecordValid
        {
            get
            {
                return mRecords != null;
            }
        }

        public LocatePage()
        {
            InitializeComponent();
        }

        private void chooseFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text documents (.txt)|*.txt";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    Records = MonthRecordCollection.LoadFile(dlg.FileName);
                    fileName.Text = dlg.FileName;
                    yearAndMonth.Text = String.Format("{0}/{1}", Records.Year, Records.Month);
                }
                catch (FileFormatException ex)
                {
                    MessageBox.Show(ex.ToString(), "文件格式错误");
                }
            }
        }

        private void nextStepClick(object sender, RoutedEventArgs e)
        {
            Page page = new CalendarPage(Records);
            this.NavigationService.Navigate(page);
        }
    }
}
