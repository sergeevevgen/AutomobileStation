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
using System.Collections.ObjectModel;

namespace AutoFixStationStoreeKeeperView
{
    /// <summary>
    /// Логика взаимодействия для WorkTypeWindow.xaml
    /// </summary>
    public partial class WorkTypeWindow : Window
    {
        private readonly IWorkTypeLogic _logic;

        private readonly ITimeOfWorkLogic _ToWlogic;
        public int Id { set { id = value; } }
        private int? id;
        public WorkTypeWindow(IWorkTypeLogic logic, ITimeOfWorkLogic ToWlogic)
        {
            InitializeComponent();
            _logic = logic;
            _ToWlogic = ToWlogic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            /*if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название запчасти", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           // if (string.IsNullOrEmpty(TextBoxFactoryNum.Text))
            {
                MessageBox.Show("Введите заводской номер запчасти", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //if (string.IsNullOrEmpty(TextBoxPrice.Text))
            {
                MessageBox.Show("Введите стоимость запчасти", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //if (string.IsNullOrEmpty(ComboBoxType.Text))
            {
                MessageBox.Show("Введите тип запчасти", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //if (string.IsNullOrEmpty(ComboBoxMeasurement.Text))
            {
                MessageBox.Show("Введите единицы измерения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }*/

            try
            {
                _logic.CreateOrUpdate(new WorkTypeBindingModel
                {
                    Id = id,
                    WorkName = TextBoxName.Text,
                    Price = Convert.ToDecimal(TextBoxPrice.Text),
                    //Type = (SparePartStatus)Enum.Parse(typeof(SparePartStatus), ComboBoxType.SelectedValue.ToString()),
                    //UMeasurement = (UnitMeasurement)Enum.Parse(typeof(UnitMeasurement), ComboBoxMeasurement.SelectedValue.ToString())
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (id != null)
            {
                var lunch = _logic.Read(new WorkTypeBindingModel
                {
                    Id = id
                })[0];
                var listToW = _ToWlogic.Read(null);
                foreach (var tow    in listToW)
                {
                    ListBoxTimeOfWorks.Items.Add(tow);
                }
                //TextBoxName.Text = lunch.Name.ToString();
                //TextBoxFactoryNum.Text = lunch.FactoryNumber.ToString();
                //TextBoxPrice.Text = lunch.Price.ToString();
                //ComboBoxType.DataContext = Enum.GetValues(typeof(SparePartStatus));
                //ComboBoxMeasurement.DataContext = Enum.GetValues(typeof(UnitMeasurement));

            }
        }
    }
}
