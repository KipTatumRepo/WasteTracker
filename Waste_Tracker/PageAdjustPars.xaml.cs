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
    /// Interaction logic for PageAdjustPars.xaml
    /// </summary>
    public partial class PageAdjustPars : Page
    {
        public PageAdjustPars()
        {
            InitializeComponent();

            //Load database 
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));

            //Load combobox of stations
            CollectionViewSource stViewSource = ((CollectionViewSource)(FindResource("wasteTrackerStationsViewSource")));
            SandboxDataSetTableAdapters.WasteTrackerStationsTableAdapter sbda = new SandboxDataSetTableAdapters.WasteTrackerStationsTableAdapter();
            sbda.Fill(ds.WasteTrackerStations);
        }

        private void wasteTrackerStationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            //get index of combobox selected item 0 based
            int item = wasteTrackerStationsComboBox.SelectedIndex;
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();

            //fill datagrid with dataset of menu items that match station selection
            da.FillByStation(ds.WasteTrackerDB, item);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();
            try
            {
                da.Update(ds.WasteTrackerDB);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops there was a problem, please contact Business Intelligence \n" + ex);
            }
        }
    }
}
