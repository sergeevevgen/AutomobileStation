﻿using System;
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
using AutoFixStationContracts.BusinessLogicsContracts;
using Unity;
namespace AutoFixStationStoreeKeeperView
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private readonly IStoreKeeperLogic _logic;
        public AuthorizationWindow(IStoreKeeperLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void ToSignUp_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<RegistrationWindow>();
            form.ShowDialog();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPassword.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var headwaiters = _logic.Read(null);
            StoreKeeperViewModel _headwaiter = null;

            foreach (var hw in headwaiters)
            {
                if (hw.Login == TextBoxLogin.Text && hw.Password == TextBoxPassword.Text)
                {
                    _headwaiter = hw;
                }
            }

            if (_headwaiter != null)
            {
                App.StoreKeeper = _headwaiter;
                var form = App.Container.Resolve<MainWindow>();
                Close();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неверно введён логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}