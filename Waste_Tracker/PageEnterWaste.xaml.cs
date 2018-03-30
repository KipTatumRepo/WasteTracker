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
using System.Data.Entity;

namespace Waste_Tracker
{
    /// <summary>
    /// Interaction logic for PageEnterWaste.xaml
    /// </summary>
    /// 

   

    public partial class PageEnterWaste : Page
    {
        SandboxEntities context;
        public PageEnterWaste()
        {
            InitializeComponent();

            //Load database 
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));

            //Load combobox of stations
            CollectionViewSource stViewSource = ((CollectionViewSource)(FindResource("wasteTrackerStationsViewSource")));
            SandboxDataSetTableAdapters.WasteTrackerStationsTableAdapter sbda = new SandboxDataSetTableAdapters.WasteTrackerStationsTableAdapter();
            sbda.Fill(ds.WasteTrackerStations);

            //Load datagrid of menu items
            CollectionViewSource miViewSource = ((CollectionViewSource)(FindResource("wasteTrackerDBViewSource")));
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();
            da.Fill(ds.WasteTrackerDB);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();
            da.Update(ds.WasteTrackerDB);
        }

        private void wasteTrackerStationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            context = new SandboxEntities();
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            var item = wasteTrackerStationsComboBox.SelectedIndex;
            MessageBox.Show(item.ToString());


            var query = (from d in context.WasteTrackerDBs
                        where d.StationId == item
                        select new
                        {
                            d.MenuItem,
                            d.LeftOver,
                            d.Par,
                            d.UoM,
                        }).ToList();

            wasteTrackerDBDataGrid.ItemsSource = query;
            //lost on what to do here????????????
        }
    }


}
