using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using System;
using System.Windows;

namespace WpfStoreKeeper
{
    /// <summary>
    /// Логика взаимодействия для TimeOfWorkWindow.xaml
    /// </summary>
    public partial class TimeOfWorkWindow : Window
    {
        private readonly ITimeOfWorkLogic _logic;
        public int Id { set { id = value; } }
        private int? id;

        public TimeOfWorkWindow(ITimeOfWorkLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxHours.Text))
            {
                MessageBox.Show("Введите количество часов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxMins.Text))
            {
                MessageBox.Show("Введите количество минут", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _logic.CreateOrUpdate(new TimeOfWorkBindingModel
                {
                    Id = id,
                    Hours = Convert.ToInt32(TextBoxHours.Text),
                    Mins = Convert.ToInt32(TextBoxMins.Text),
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
                var lunch = _logic.Read(new TimeOfWorkBindingModel
                {
                    Id = id
                })[0];


                TextBoxHours.Text = lunch.Hours.ToString();
                TextBoxMins.Text = lunch.Mins.ToString();
            }
        }
    }
}