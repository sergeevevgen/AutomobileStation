using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.Enums;
using System;
using System.Collections.ObjectModel;
using System.Windows;

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

#pragma warning disable CS8618 // поле "StatusList", не допускающий значения NULL, должен содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающий значения NULL.
        public SparePartWindow(ISparePartLogic logic)
#pragma warning restore CS8618 // поле "StatusList", не допускающий значения NULL, должен содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающий значения NULL.
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
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL, для параметра "value" в "object Enum.Parse(Type enumType, string value)".
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL, для параметра "value" в "object Enum.Parse(Type enumType, string value)".
                _logic.CreateOrUpdate(new SparePartBindingModel
                {
                    Id = id,
                    Name = TextBoxName.Text,
                    FactoryNumber = TextBoxFactoryNum.Text,
                    Price = Convert.ToDecimal(TextBoxPrice.Text),
                    Type = (SparePartStatus)Enum.Parse(typeof(SparePartStatus), ComboBoxType.SelectedValue.ToString()),
                    UMeasurement = (UnitMeasurement)Enum.Parse(typeof(UnitMeasurement), ComboBoxMeasurement.SelectedValue.ToString())
                });
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL, для параметра "value" в "object Enum.Parse(Type enumType, string value)".
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL, для параметра "value" в "object Enum.Parse(Type enumType, string value)".
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
