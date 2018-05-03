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
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace Waste_Tracker.Pages
{
    /// <summary>
    /// Interaction logic for PageViewReports.xaml
    /// </summary>
    public partial class PageViewReports 
    {
        
        public PageViewReports()
        {
            InitializeComponent();
            try
            {
                //Load database 
                SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));

                //Load combobox of stations
                CollectionViewSource stViewSource = ((CollectionViewSource)(FindResource("wasteTrackerStationsViewSource")));
                SandboxDataSetTableAdapters.WasteTrackerStationsTableAdapter sbda = new SandboxDataSetTableAdapters.WasteTrackerStationsTableAdapter();
                sbda.Fill(ds.WasteTrackerStations);
            }
            catch (Exception ex)
            {
                BIMessageBox.Show("Oops there was a problem, please contact Business Intelligence \n" + ex);
            }
        }

        private void wasteTrackerStationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));

            //get index of combobox selected item 0 based
            int item = wasteTrackerStationsComboBox.SelectedIndex;
            string date = dateDatePicker.SelectedDate.ToString();
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();
            
            //fill datagrid with dataset of menu items that match station selection
            da.FillByStation3(ds.WasteTrackerDB, item, date);

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            //create new excel application
            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            //create workbook and worksheet
            Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet ws = (Worksheet)xla.ActiveSheet;

            //start at row 2 row 1 is the header
            int i = 2;

            //create header
            ws.Range["A1"].Cells.ColumnWidth = 24;
            ws.Range["A1"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter; 
            ws.Range["A1"].Value = "Menu Item";
            ws.Range["B1"].Cells.ColumnWidth = 24;
            ws.Range["B1"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["B1"].Value = "Percentage of Consumed";
            ws.Range["C1"].Cells.ColumnWidth = 24;
            ws.Range["C1"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["C1"].Value = "Percentage of Waste";

            //iterate through datagrid and put into excel doc
            foreach (DataRow dr in ds.WasteTrackerDB.Rows)
            {
               
                ws.Range["A"+i].Value = dr[2];
                ws.Range["B"+i].Value = dr[9];
                ws.Range["C"+i].Value = dr[10];
                i++;
            }
            xla.Visible = true;
            

        }
    }
}
