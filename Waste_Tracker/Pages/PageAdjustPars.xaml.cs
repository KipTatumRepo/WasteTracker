﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        SqlConnection Conn;
        SqlCommand Cmd;

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
        #region Combobox Selection
        private void wasteTrackerStationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            //get index of combobox selected item 0 based
            int item = wasteTrackerStationsComboBox.SelectedIndex;
            SandboxDataSetTableAdapters.MenuItemsTableAdapter mida = new SandboxDataSetTableAdapters.MenuItemsTableAdapter();

            //fill datagrid with dataset of menu items that match station selection
            mida.FillByStation(ds.MenuItems, item);
        }
        #endregion
        #region Button Clicks
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SandboxDataSet ds = ((SandboxDataSet)(FindResource("sandboxDataSet")));
            //SandboxDataSetTableAdapters.MenuItemsTableAdapter mida = new SandboxDataSetTableAdapters.MenuItemsTableAdapter();
            
            try
            {

                //Conn = new SqlConnection("Data Source=compasspowerbi;Initial Catalog=Sandbox;Persist Security Info=False;Integrated Security=SSPI");
                Conn = new SqlConnection("Data Source=compassbiazure.database.windows.net;Initial Catalog=FieldSiteDB;Persist Security Info=True;User ID=FieldApps;Password=K%Th8#30!");
                Conn.Open();

                //iterate over datagrid, update values
                foreach (DataRow dr in ds.MenuItems.Rows)
                {
                    /*string stationId = dr[0].ToString();
                    int stationid = int.Parse(stationId);
                    string MenuItem = dr[1].ToString();
                    string par = dr[2].ToString();
                    decimal Par = decimal.Parse(par);
                    string UoM = dr[3].ToString();
                    string isactive = dr[4].ToString();
                    int IsActive = int.Parse(isactive);*/

                    string sqlString = "UPDATE MenuItems SET Par = @Par, UoM = @UoM, IsActive = @IsActive WHERE MenuItem = @MenuItem AND @StationId = StationId";
                    Cmd = new SqlCommand(sqlString, Conn);
                    Cmd.Parameters.AddWithValue("@StationId", dr[1]);
                    Cmd.Parameters.AddWithValue("@MenuItem", dr[2]);
                    Cmd.Parameters.AddWithValue("@Par", dr[3]);
                    Cmd.Parameters.AddWithValue("@UoM", dr[4]);
                    Cmd.Parameters.AddWithValue("@IsActive", dr[5]);
                    
                    //get par cell for data validation
                    /*string _par = dr[2].ToString();
                    decimal par;
                    par = decimal.Parse(_par);

                    if (par < 0)
                    {
                        BIMessageBox.Show("Please a positive number for the Par Amount.");
                        return;
                    }
                    else
                    {*/
                        //mida.UpdateQuery(stationid, MenuItem, Par, UoM, IsActive);
                        Cmd.ExecuteNonQuery();
                    //}
                }
                Conn.Close();
                BIMessageBox.Show("Par level has been updated.");
            }
            catch (Exception ex)
            {
                BIMessageBox.Show("Oops there was a problem, please contact Business Intelligence \n" + ex);
            }
        }
        #endregion
    }
}
