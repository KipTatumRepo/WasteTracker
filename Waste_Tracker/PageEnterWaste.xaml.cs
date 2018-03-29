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
    public partial class PageEnterWaste : Page
    {
        //SandboxEntities sbe = new SandboxEntities();
        //CollectionViewSource miViewSource;
        public PageEnterWaste()
        {
            InitializeComponent();
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();
            da.Fill(ds.WasteTrackerDB);
            CollectionViewSource miViewSource = ((CollectionViewSource)(FindResource("wasteTrackerDBViewSource")));
            

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            SandboxDataSetTableAdapters.WasteTrackerStationsTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerStationsTableAdapter();
            da.Fill(ds.WasteTrackerStations);
            //sbe.WasteTrackerDBs.Load();
            //miViewSource.Source = sbe.WasteTrackerDBs.Local;
            
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();
            da.Update(ds.WasteTrackerDB);
            
        }
    }
}
