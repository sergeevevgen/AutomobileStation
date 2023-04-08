using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using System;
using System.Windows;


namespace AutoFixStationStoreeKeeperView
{
    /// <summary>
    /// Логика взаимодействия для WorksWindow.xaml
    /// </summary>
    public partial class WorksWindow : Window
    {
        private readonly IWorkLogic _worklogic;
        private readonly ITOLogic _toLogic;
        private readonly IServiceRecordLogic _servRecLogic;

        public WorksWindow(IWorkLogic logic, ITOLogic toLogic, IServiceRecordLogic servRecLogic)
        {
            InitializeComponent();
            _worklogic = logic;
            _toLogic = toLogic;
            _servRecLogic = servRecLogic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataGridWorks.Items.Clear();
#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
                var listWorks = _worklogic.Read(null);
#pragma warning restore CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
                foreach (var sp in listWorks)
                {
                    DataGridWorks.Items.Add(sp);

                }
                /*var list = _worklogic.Read(new WorkBindingModel
                {
                    StoreKeeperId = (int)App.StoreKeeper.Id
                });

                if (list != null)
                {
                    DataGridRoomers.ItemsSource = list;
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ButtonStartWork_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridWorks.SelectedItem != null)
            {
                WorkViewModel selWork = (WorkViewModel)DataGridWorks.SelectedItem;
                _worklogic.TakeWorkInWork(new ChangeWorkStatusBindingModel
                {
                    WorkId = selWork.Id
                });
            }
        }

        private void ButtonFinish_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridWorks.SelectedItem != null)
            {
                WorkViewModel selWork = (WorkViewModel)DataGridWorks.SelectedItem;
                _worklogic.FinishWork(new ChangeWorkStatusBindingModel
                {
                    WorkId = selWork.Id
                });
            }

        }
    }
}
