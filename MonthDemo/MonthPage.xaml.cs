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
    /// Interaction logic for MonthPage.xaml
    /// </summary>
    public partial class MonthPage : Page
    {
        public MonthPage()
        {
            InitializeComponent();
            myItems.ItemsSource = new Unit[]{
                new Unit{ Text="1", GridRow=0, GridColumn=0 },
                new Unit{ Text="2", GridRow=0, GridColumn=1 },
                new Unit{ Text="3", GridRow=0, GridColumn=2 },
                new Unit{ Text="4", GridRow=1, GridColumn=0 },
                new Unit{ Text="5", GridRow=1, GridColumn=1 },
                new Unit{ Text="6", GridRow=1, GridColumn=2 },
                new Unit{ Text="7", GridRow=2, GridColumn=0 },
                new Unit{ Text="8", GridRow=2, GridColumn=1 },
                new Unit{ Text="9", GridRow=2, GridColumn=2 },
            };
        }
    }
}
