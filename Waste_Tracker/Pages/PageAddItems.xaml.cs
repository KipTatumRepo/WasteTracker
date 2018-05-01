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

namespace Waste_Tracker
{
    /// <summary>
    /// Interaction logic for PageAddItems.xaml
    /// </summary>
    public partial class PageAddItems : Page
    {
        int StationId;
        string MenuItem;
        decimal Par;
        string UoM;
        int IsActive;
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
            Par = decimal.Parse(parTextBox.Text);
            IsActive = 1;
            UoM = uoMTextBox.Text;
            
            SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter da = new SandboxDataSetTableAdapters.WasteTrackerDBTableAdapter();

            try
            { 
            //Custom Insert Statement
                da.InsertQuery(StationId, MenuItem, Par, UoM, IsActive);
                BIMessageBox.Show("New Menu Item Added");
            }
            catch (Exception ex)
            {
                BIMessageBox.Show("Oops there was a problem, please contact Business Intelligence \n" + ex.Message);
            }

            //clear textbox 
            wasteTrackerStationsComboBox.SelectedIndex = 0;
            menuItemTextBox.Clear();
            parTextBox.Clear();
            uoMTextBox.Clear();
        }
    }
}
