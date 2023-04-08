﻿using System.Windows;
using Unity;

namespace AutoFixStationStoreeKeeperView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MenuItemTimeOfWork_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<TimeOfWorksWindow>();
            form.ShowDialog();
        }

        private void MenuItemSPWorkType_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<LinkSparePartsToWorkTypeWindow>();
            form.ShowDialog();
        }

        private void MenuItemSpareParts_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<SparePartsWindow>();
            form.ShowDialog();
        }

        private void MenuItemWorks_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WorksWindow>();
            form.ShowDialog();
        }

        private void MenuWorkTypesList_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<WorkTypesWindow>();
            form.ShowDialog();
        }

        private void MenuItemReport_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<ReportSparePartsWindow>();
            form.ShowDialog();
        }

        private void MenuItemGetList_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<GetSparePartsListWindow>();
            form.ShowDialog();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            App.StoreKeeper = null;

            var windowSignIn = App.Container.Resolve<AuthorizationWindow>();
            Close();
            windowSignIn.ShowDialog();
        }
    }
}