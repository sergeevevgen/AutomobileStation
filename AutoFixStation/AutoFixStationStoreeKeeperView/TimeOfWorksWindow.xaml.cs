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
using AutoFixStationContracts.ViewModels;
using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using Unity;

namespace AutoFixStationStoreeKeeperView
{
    /// <summary>
    /// Логика взаимодействия для TimeOfWorksWindow.xaml
    /// </summary>
    public partial class TimeOfWorksWindow : Window
    {
        private readonly ITimeOfWorkLogic _logic;
        public TimeOfWorksWindow(ITimeOfWorkLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var form = App.Container.Resolve<TimeOfWorkWindow>();
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

        private void TimeOfWorksWindow_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = _logic.Read(null);
                if (list != null)
                {
                    DataGridTimeOfWorks.ItemsSource = list;
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
                if (DataGridTimeOfWorks.SelectedItems.Count == 1)
                {
                    var form = App.Container.Resolve<TimeOfWorkWindow>();
                    form.Id = ((TimeOfWorkViewModel)DataGridTimeOfWorks.SelectedItems[0]).Id;
                    
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
            if (DataGridTimeOfWorks.SelectedItems.Count == 1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    int id = ((TimeOfWorkViewModel)DataGridTimeOfWorks.SelectedItems[0]).Id;

                    try
                    {
                        _logic.Delete(new TimeOfWorkBindingModel { Id = id });
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