using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Waste_Tracker.Pages
{
    /// <summary>
    /// Interaction logic for PagePrintSheets.xaml
    /// </summary>
    public partial class PagePrintSheets 
    {
        public PagePrintSheets()
        {
            InitializeComponent();

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
            //SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();
            SandboxDataSetTableAdapters.MenuItemsTableAdapter mida = new SandboxDataSetTableAdapters.MenuItemsTableAdapter();

            //fill datagrid with dataset of menu items that match station selection
            mida.FillByStation(ds.MenuItems, item);
        }


        private void Button_Click_PrintStation(object sender, RoutedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            CollectionViewSource stViewSource = ((CollectionViewSource)(FindResource("wasteTrackerStationsViewSource")));
           
            //create new excel application
            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();

            //create workbook and worksheet
            Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet ws = (Worksheet)xla.ActiveSheet;
            ws.PageSetup.FitToPagesWide = 1;
            ws.PageSetup.Orientation = XlPageOrientation.xlLandscape;

            //start at row 3 row 1 is the Station, row 3 is header
            int i = 3;

            //create header
            //ws.Range["A1"].Cells.Value = wasteTrackerStationsComboBox.SelectedItem.ToString();
            ws.Range["A2"].Cells.ColumnWidth = 8;
            ws.Range["A2"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["A2"].Cells.Borders.LineStyle =  Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ws.Range["A2"].Cells.Rows.RowHeight = 30;
            ws.Range["A2"].Value = "Station Id";
            ws.Range["B2"].Cells.ColumnWidth = 26;
            ws.Range["B2"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["B2"].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ws.Range["B2"].Value = "Menu Item";
            ws.Range["C2"].Cells.ColumnWidth = 4;
            ws.Range["C2"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["C2"].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ws.Range["C2"].Value = "UoM";
            ws.Range["D2"].Cells.ColumnWidth = 6.2;
            ws.Range["D2"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["D2"].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ws.Range["D2"].Value = "Mon";
            ws.Range["E2"].Cells.ColumnWidth = 6.2;
            ws.Range["E2"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["E2"].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ws.Range["E2"].Value = "Tue";
            ws.Range["F2"].Cells.ColumnWidth = 6.2;
            ws.Range["F2"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["F2"].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ws.Range["F2"].Value = "Wed";
            ws.Range["G2"].Cells.ColumnWidth = 6.2;
            ws.Range["G2"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["G2"].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ws.Range["G2"].Value = "Thu";
            ws.Range["H2"].Cells.ColumnWidth = 6.2;
            ws.Range["H2"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["H2"].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ws.Range["H2"].Value = "Fri";
            ws.Range["I2"].Cells.ColumnWidth = 40;
            ws.Range["I2"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["I2"].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            ws.Range["I2"].Value = "Comments";

            foreach (DataRow dr in ds.MenuItems.Rows)
            {
                ws.Range["A" + i].Value = dr[0];
                ws.Range["A" + i].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                ws.Range["A" + i].Cells.Rows.RowHeight = 30;
                ws.Range["B" + i].Value = dr[1];
                ws.Range["B" + i].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                ws.Range["C" + i].Value = dr[3];
                ws.Range["C" + i].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                ws.Range["D" + i].Value = "";
                ws.Range["D" + i].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                ws.Range["E" + i].Value = "";
                ws.Range["E" + i].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                ws.Range["F" + i].Value = "";
                ws.Range["F" + i].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                ws.Range["G" + i].Value = "";
                ws.Range["G" + i].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                ws.Range["H" + i].Value = "";
                ws.Range["H" + i].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                ws.Range["I" + i].Value = "";
                ws.Range["I" + i].Cells.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                i++;
            }
            
            xla.Visible = true;
        }
    }
}
