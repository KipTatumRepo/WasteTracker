using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for PageActivateItem.xaml
    /// </summary>
    public partial class PageActivateItem : Page
    {
        SqlConnection Conn;
        SqlCommand Cmd;
        public PageActivateItem()
        {
            InitializeComponent();

            //Load database 
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));

            //Load combobox of stations
            CollectionViewSource stViewSource = ((CollectionViewSource)(FindResource("wasteTrackerStationsViewSource")));
            SandboxDataSetTableAdapters.WasteTrackerStationsTableAdapter sbda = new SandboxDataSetTableAdapters.WasteTrackerStationsTableAdapter();
            sbda.Fill(ds.WasteTrackerStations);
        }

        #region Combobox Selection
        private void wasteTrackerStationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            //get index of combobox selected item 0 based
            int item = wasteTrackerStationsComboBox.SelectedIndex;
            SandboxDataSetTableAdapters.MenuItemsTableAdapter mida = new SandboxDataSetTableAdapters.MenuItemsTableAdapter();

            //fill datagrid with dataset of menu items that match station selection
            mida.FillByStationAll(ds.MenuItems, item);
        }
        #endregion
        #region Button Clicks
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));

            Conn = new SqlConnection("Data Source=compasspowerbi;Initial Catalog=Sandbox;Persist Security Info=False;Integrated Security=SSPI");
            Conn.Open();
            
            foreach (DataRow dr in ds.MenuItems.Rows)
            {
                string sqlString = "UPDATE MenuItems SET IsActive = @IsActive WHERE MenuItem = @MenuItem";
                Cmd = new SqlCommand(sqlString, Conn);
                Cmd.Parameters.AddWithValue("@MenuItem", dr[1]);
                Cmd.Parameters.AddWithValue("@IsActive", dr[4]);
                Cmd.ExecuteNonQuery();
            }
            BIMessageBox.Show("Item is Active Again");
            Conn.Close();
        }
        #endregion
    }
}
