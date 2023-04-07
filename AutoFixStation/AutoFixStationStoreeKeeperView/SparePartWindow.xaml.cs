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
using System.ComponentModel;

namespace AutoFixStationStoreeKeeperView
{
    /// <summary>
    /// Логика взаимодействия для SparePartWindow.xaml
    /// </summary>
    public partial class SparePartWindow : Window
    {
        private readonly ISparePartLogic _logic;
        public int Id { set { id = value; } }
        private int? id;

        public ObservableCollection<SparePartStatus> StatusList;

        public SparePartWindow(ISparePartLogic logic)
        {
            InitializeComponent();
            _logic = logic;



        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название запчасти", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxFactoryNum.Text))
            {
                MessageBox.Show("Введите заводской номер запчасти", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPrice.Text))
            {
                MessageBox.Show("Введите стоимость запчасти", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(ComboBoxType.Text))
            {
                MessageBox.Show("Введите тип запчасти", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(ComboBoxMeasurement.Text))
            {
                MessageBox.Show("Введите единицы измерения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _logic.CreateOrUpdate(new SparePartBindingModel
                {
                    Id = id,
                    Name = TextBoxName.Text,
                    FactoryNumber = TextBoxFactoryNum.Text,
                    Price = Convert.ToDecimal(TextBoxPrice.Text),
                    Type = (SparePartStatus)Enum.Parse(typeof(SparePartStatus), ComboBoxType.SelectedValue.ToString()),
                    UMeasurement = (UnitMeasurement)Enum.Parse(typeof(UnitMeasurement), ComboBoxMeasurement.SelectedValue.ToString())
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
                var sp = _logic.Read(new SparePartBindingModel
                {
                    Id = id
                })[0];

                TextBoxName.Text = sp.Name.ToString();
                TextBoxFactoryNum.Text = sp.FactoryNumber.ToString();
                TextBoxPrice.Text = sp.Price.ToString();


            }
        }
       
    }
}
