﻿using System;
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
        #region Combobox Selection
        private void wasteTrackerStationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get index of combobox selected item 0 based
            int item = wasteTrackerStationsComboBox.SelectedIndex;
            DateTime? StartDate = startDateDatePicker.SelectedDate;
            DateTime? EndDate = endDateDatePicker.SelectedDate;
            FillData(StartDate, EndDate, item);
            
            return;
        }

        private void endDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int item = wasteTrackerStationsComboBox.SelectedIndex;
            DateTime? StartDate = startDateDatePicker.SelectedDate;
            DateTime? EndDate = endDateDatePicker.SelectedDate;

            if (EndDate < StartDate)
            {
                BIMessageBox.Show("Please Select an End Date That is After the Start Date.");
            }
            else
            {
                FillData(StartDate, EndDate, item);
            }
        }

        private void startDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int item = wasteTrackerStationsComboBox.SelectedIndex;
            DateTime? StartDate = startDateDatePicker.SelectedDate;
            DateTime? EndDate = endDateDatePicker.SelectedDate;
            if(StartDate > EndDate)
            {
                BIMessageBox.Show("Please Select a Start Date That is Before the End Date.");
            }
            else
            { 
                if(EndDate == null)
                {
                    return;
                }
                else
                { 
                    FillData(StartDate, EndDate, item);
                }
            }
        }

        public void FillData(DateTime? start, DateTime? end, int item)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();
            string startdate = start.ToString();
            string enddate = end.ToString();
            if (startdate == null)
            {
                BIMessageBox.Show("Please Select a Date");
                return;
            }
            else
            {

                da.FillByDateRange(ds.WasteTrackerDB, item, startdate, enddate);

            }
        }
        
        #endregion
        #region Button Clicks
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            //create new excel application
            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            //create workbook and worksheet
            Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet ws = (Worksheet)xla.ActiveSheet;

            //start at row 4 row 1 is the Station, row 2 is date, row 3 is header
            int i = 4;

            //create header
            ws.Range["A1"].Cells.Value = wasteTrackerStationsComboBox.SelectedValue.ToString();
            ws.Range["A2"].Cells.Value = "Report Generated on: " + DateTime.Now; //startDateDatePicker.SelectedDate.ToString();
            ws.Range["A3"].Cells.ColumnWidth = 24;
            ws.Range["A3"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter; 
            ws.Range["A3"].Value = "Menu Item";
            ws.Range["B3"].Cells.ColumnWidth = 24;
            ws.Range["B3"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["B3"].Value = "Date";
            ws.Range["C3"].Cells.ColumnWidth = 24;
            ws.Range["C3"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["C3"].Value = "% of Par Left Over";
            ws.Range["D3"].Cells.ColumnWidth = 24;
            ws.Range["D3"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["D3"].Value = "% of Prod Left Over";
            ws.Range["E3"].Cells.ColumnWidth = 48;
            ws.Range["E3"].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Range["E3"].Cells.WrapText = true;
            ws.Range["E3"].Value = "Notes";

            //iterate through datagrid and put into excel doc
            foreach (DataRow dr in ds.WasteTrackerDB.Rows)
            {
                ws.Range["A" + i].Value = dr[2];
                ws.Range["B" + i].Value = dr[6];
                ws.Range["C" + i].Value = dr[9];
                ws.Range["D" + i].Value = dr[10];
                ws.Range["E" + i].Value = dr[12];
                i++;
            }
            xla.Visible = true;
        }

        #endregion
    }
}
