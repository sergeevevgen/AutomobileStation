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
using AutoFixStationBusinessLogic.BusinessLogics;
using AutoFixStationContracts.ViewModels;
using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using Unity;

namespace StoreKeeperView
{
    /// <summary>
    /// Логика взаимодействия для SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private readonly IStoreKeeperLogic _logic;
        public SignUpWindow(IStoreKeeperLogic logic)
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
