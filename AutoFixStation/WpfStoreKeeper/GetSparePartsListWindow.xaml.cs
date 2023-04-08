using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfStoreKeeper
{
    /// <summary>
    /// Логика взаимодействия для GetSparePartsListWindow.xaml
    /// </summary>
    public partial class GetSparePartsListWindow : Window
    {

        private readonly IWorkTypeLogic _workTypeLogic;
        private readonly IStoreKeeperReportLogic _reportLogic;

        public GetSparePartsListWindow(IWorkTypeLogic workTypeLogic, IStoreKeeperReportLogic reportLogic)
        {
            InitializeComponent();
            _workTypeLogic = workTypeLogic;
            _reportLogic = reportLogic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var listWT = _workTypeLogic.Read(null);

            foreach (var sp in listWT)
            {
                ListBoxWorkType.Items.Add(sp);

            }
        }

        private void ButtonExcel_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxWorkType.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите работы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SaveFileDialog dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var list = new List<WorkTypeViewModel>();

                    foreach (var room in ListBoxWorkType.SelectedItems)
                    {
                        list.Add((WorkTypeViewModel)room);
                    }

                    _reportLogic.SaveWorkTypesToExcelFile(new ReportSparePartBindingModel
                    {
                        FileName = dialog.FileName,
                        WorkTypes = list
                    });

                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonWord_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxWorkType.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите работы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            try
            {
                if (dialog.ShowDialog() == true)
                {
                    var list = new List<WorkTypeViewModel>();

                    foreach (var room in ListBoxWorkType.SelectedItems)
                    {
                        list.Add((WorkTypeViewModel)room);
                    }

                    _reportLogic.SaveWorkTypesToWordFile(new ReportSparePartBindingModel
                    {
                        FileName = dialog.FileName,
                        WorkTypes = list
                    });

                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
