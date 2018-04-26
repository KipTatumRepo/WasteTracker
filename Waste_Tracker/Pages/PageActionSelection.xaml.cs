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
    /// Interaction logic for PageActionSelection.xaml
    /// </summary>
    public partial class PageActionSelection : Page
    {
        public PageActionSelection()
        {
            InitializeComponent();
        }

        private void EnterWaste_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new Uri("Pages/PageEnterWaste.xaml", UriKind.Relative));
        }

        private void AdjustPars_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new Uri("Pages/PageAdjustPars.xaml", UriKind.Relative));
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new Uri("Pages/PageAddItems.xaml", UriKind.Relative));
        }
    }
}