﻿using System;
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
using System.Windows.Controls.Primitives;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Waste_Tracker
{
    /// <summary>
    /// Interaction logic for PageEnterWaste.xaml
    /// </summary>

    public partial class PageEnterWaste : Page
    {
        //SqlConnection Conn;
        SqlCommand Cmd;

        public PageEnterWaste()
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
                System.Windows.MessageBox.Show("Oops there was a problem, please contact Business Intelligence \n" + ex);
            }
        }

        #region Combobox Selection
        private void wasteTrackerStationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));

            //get index of combobox selected item 0 based
            int item = wasteTrackerStationsComboBox.SelectedIndex;
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();

            //fill datagrid with dataset of menu items that match station selection
            da.FillByStation2(ds.WasteTrackerDB, item);
        }
        #endregion

        #region Button Clicks
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();
            DateTime? Date = dateDatePicker.SelectedDate;
           
            //if there is a date selected
            if(Date != null)
            {
                try
                {
                    //use SQL command to insert into DB
                    SqlConnection conn = ConnectionHelper.GetConn();
                    conn.Open();
                    decimal LeftOver;
                    decimal Ordered;

                    //iterate over each row of DataGrid, get values of each cell, and insert into DB
                    foreach (DataRow dr in ds.WasteTrackerDB.Rows)
                    {
                        string sqlString = "INSERT INTO WasteTrackerDB VALUES (@StationId, @MenuItem, @LeftOver, @Par, @UoM, @Date, @IsActive, @Ordered, @Notes)";
                        Cmd = new SqlCommand(sqlString, conn);
                        Cmd.Parameters.AddWithValue("@StationId", dr[1]);
                        Cmd.Parameters.AddWithValue("@MenuItem", dr[2]);
                        Cmd.Parameters.AddWithValue("@LeftOver", dr[3]);
                        Cmd.Parameters.AddWithValue("@Par", dr[4]);
                        Cmd.Parameters.AddWithValue("@UoM", dr[5]);
                        Cmd.Parameters.AddWithValue("@Date", Date);
                        Cmd.Parameters.AddWithValue("@IsActive", 1);
                        Cmd.Parameters.AddWithValue("@Ordered", dr[8]);
                        Cmd.Parameters.AddWithValue("@Notes", dr[12]);

                        string leftOver = dr[3].ToString();
                        string ordered = dr[8].ToString();
                        
                        if (leftOver == "" || ordered == "" )
                        {
                            BIMessageBox.Show("Please enter a valid number between 0 and 99999999");
                            return;
                        }
                        else
                        { 
                        LeftOver = decimal.Parse(leftOver);
                        Ordered = decimal.Parse(ordered);
                            if (LeftOver < 0 || Ordered < 0 || LeftOver == -1 || Ordered == -1 )
                            {
                                BIMessageBox.Show("Please enter a valid number between 0 and 9999999999");
                                return;
                            }
                            else
                            {
                                Cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    BIMessageBox.Show("Leftover values have been added");
                    conn.Close();
                }
                catch (Exception ex)
                {
                    BIMessageBox.Show("Oops there was a problem, please contact Business Intelligence \n" + ex);
                }
            }
            //oops there was no date selected
            else
            {
                BIMessageBox.Show("Please Enter a Date");
                return;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();

            DateTime? Date = dateDatePicker.SelectedDate;

            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();

            //iterate over datagrid, update values
            foreach (DataRow dr in ds.WasteTrackerDB.Rows)
            {
                string sqlString = "UPDATE WasteTrackerDB SET LeftOver = @LeftOver, Ordered = @Ordered WHERE MenuItem = @MenuItem AND Date = @Date";
                Cmd = new SqlCommand(sqlString, conn);
                Cmd.Parameters.AddWithValue("@MenuItem", dr[2]);
                Cmd.Parameters.AddWithValue("@LeftOver", dr[3]);
                Cmd.Parameters.AddWithValue("@Ordered", dr[8]);
                Cmd.Parameters.AddWithValue("@Date", Date);
                Cmd.ExecuteNonQuery();
            }
            BIMessageBox.Show("Leftover values have been updated");
            conn.Close();
        }
        #endregion
    }
}
