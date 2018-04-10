using System;
using System.Collections.Generic;
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

namespace Waste_Tracker
{
    /// <summary>
    /// Interaction logic for PageAddItems.xaml
    /// </summary>
    public partial class PageAddItems : Page
    {
        int StationId;
        string MenuItem;
        public PageAddItems()
        {
            InitializeComponent();

            //Load database 
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));

            //Load combobox of stations
            CollectionViewSource stViewSource = ((CollectionViewSource)(FindResource("wasteTrackerStationsViewSource")));
            SandboxDataSetTableAdapters.WasteTrackerStationsTableAdapter sbda = new SandboxDataSetTableAdapters.WasteTrackerStationsTableAdapter();
            sbda.Fill(ds.WasteTrackerStations);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StationId = wasteTrackerStationsComboBox.SelectedIndex;
            MenuItem = menuItemTextBox.Text;
            string par = parTextBox.Text;
            
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();
            da.InsertQuery(StationId, MenuItem);

            MessageBox.Show("New Menu Item Added");

            menuItemTextBox.Clear();
        }

       
    }
}
