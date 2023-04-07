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

        private readonly ISparePartLogic _splogic;
        public int Id { set { id = value; } }
        private int? id;
        public WorkTypeWindow(IWorkTypeLogic logic, ITimeOfWorkLogic ToWlogic, ISparePartLogic splogic)
        {
            InitializeComponent();
            _logic = logic;
            _ToWlogic = ToWlogic;
            _splogic = splogic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название типа работы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPrice.Text))
            {
                MessageBox.Show("Введите стоимость работы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxToW.SelectedItem == null)
            {
                MessageBox.Show("Выберите время работы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            TimeOfWorkViewModel timeOfWork = (TimeOfWorkViewModel)ComboBoxToW.SelectedItem;
            decimal netPrice = 0;

            Dictionary<int, (string, decimal, decimal)> sparePartsId = new Dictionary<int, (string, decimal, decimal)>();
            foreach (SparePartViewModel sparepart in ListBoxSpareParts.SelectedItems)
            {
                sparePartsId.Add(sparepart.Id, (sparepart.Name, sparepart.Price, sparepart.Price));
                netPrice += sparepart.Price;
            }

            try
            {
                _logic.CreateOrUpdate(new WorkTypeBindingModel
                {
                    Id = id,
                    WorkName = TextBoxName.Text,
                    Price = Convert.ToDecimal(TextBoxPrice.Text),
                    NetPrice = Convert.ToDecimal(TextBoxPrice.Text) + netPrice,
                    TimeOfWorkId = timeOfWork.Id,
                    //WorkSpareParts = null
                    WorkSpareParts = sparePartsId
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
            var listToW = _ToWlogic.Read(null);
            foreach (var tow in listToW)
            {
                ComboBoxToW.Items.Add(tow);
            }

            

            var listSpareParts = _splogic.Read(null);
            foreach (var sp in listSpareParts)
            {
                ListBoxSpareParts.Items.Add(sp);
           
            }
            


        }
    }
}
