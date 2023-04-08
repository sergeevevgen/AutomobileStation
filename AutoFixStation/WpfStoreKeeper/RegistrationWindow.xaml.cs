using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using System;
using System.Windows;

namespace WpfStoreKeeper
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {

        private readonly IStoreKeeperLogic _logic;
        public RegistrationWindow(IStoreKeeperLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text))
            {
                MessageBox.Show("Введите логин - ваша электронная почта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPassword.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxFullName.Text))
            {
                MessageBox.Show("Введите полное имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _logic.CreateOrUpdate(new StoreKeeperBindingModel
                {
                    FIO = TextBoxFullName.Text,
                    Login = TextBoxLogin.Text,
                    Password = TextBoxPassword.Text,
                });
                MessageBox.Show("Вы успешно зарегистрировались", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
