using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using System;
using System.Linq;
using System.Windows;
using Unity;


namespace AutoFixStationStoreeKeeperView
{
    /// <summary>
    /// Логика взаимодействия для SparePartsWindow.xaml
    /// </summary>
    public partial class SparePartsWindow : Window
    {
        private readonly ISparePartLogic _logic;
        public SparePartsWindow(ISparePartLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var form = App.Container.Resolve<SparePartWindow>();
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SparePartsWindow_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
                var list = _logic.Read(null);
#pragma warning restore CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
                if (list != null)
                {
                    DataGridSpareParts.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridSpareParts.SelectedItems.Count == 1)
                {
                    var form = App.Container.Resolve<SparePartWindow>();
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                    form.Id = ((SparePartViewModel)DataGridSpareParts.SelectedItems[0]).Id;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.

                    if (form.ShowDialog() == true)
                    {
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridSpareParts.SelectedItems.Count == 1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                    int id = ((SparePartViewModel)DataGridSpareParts.SelectedItems[0]).Id;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.

                    try
                    {
                        _logic.Delete(new SparePartBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}