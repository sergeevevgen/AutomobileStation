using System;
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
using System.Windows.Shapes;
using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using AutoFixStationContracts.Enums;
using Unity;


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
                var listWorks = _worklogic.Read(null);
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
