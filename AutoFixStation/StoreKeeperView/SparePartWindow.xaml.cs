using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
using AutoFixStationContracts.Enums;
using AutoFixStationContracts.ViewModels;
using Unity;

namespace StoreKeeperView
{
    /// <summary>
    /// Логика взаимодействия для SparePartWindow.xaml
    /// </summary>
    public partial class SparePartWindow : Window
    {
        private readonly ISparePartLogic _logic;
        public int Id { set { id = value; } }
        private int? id;

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
            if (string.IsNullOrEmpty(TextBoxFactoryNumber.Text))
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
            if (string.IsNullOrEmpty(ComboBoxUMeasurement.Text))
            {
                MessageBox.Show("Введите единицу измерения запчасти", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _logic.CreateOrUpdate(new SparePartBindingModel
                {
                    Id = id,
                    Name = TextBoxName.Text,
                    FactoryNumber = TextBoxFactoryNumber.Text,
                    Price = System.Convert.ToDecimal(TextBoxPrice.Text),
                    Type = (SparePartStatus)Enum.Parse(typeof(SparePartStatus), ComboBoxType.Text),
                    UMeasurement = (UnitMeasurement)Enum.Parse(typeof(UnitMeasurement), ComboBoxUMeasurement.Text)
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
                var lunch = _logic.Read(new SparePartBindingModel
                {
                    Id = id
                })[0];


                TextBoxName.Text = lunch.Name;
                TextBoxFactoryNumber.Text = lunch.FactoryNumber;
                lunch.Price = System.Convert.ToDecimal(TextBoxPrice.Text);
                //Сделать Type
                //Сделать UMeasurement
            }
        }
    }
}

