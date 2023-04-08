using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfStoreKeeper
{
    /// <summary>
    /// Логика взаимодействия для LinkSparePartsToWorkTypeWindow.xaml
    /// </summary>
    public partial class LinkSparePartsToWorkTypeWindow : Window
    {
        private readonly IWorkTypeLogic _workTypeLogic;
        private readonly ISparePartLogic _sparePartLogic;

        public LinkSparePartsToWorkTypeWindow(IWorkTypeLogic workTypeLogic, ISparePartLogic sparePartLogic)
        {
            InitializeComponent();
            _workTypeLogic = workTypeLogic;
            _sparePartLogic = sparePartLogic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var listToW = _workTypeLogic.Read(null);

            foreach (var tow in listToW)
            {
                ComboBoxWorkType.Items.Add(tow);
            }


            var listSpareParts = _sparePartLogic.Read(null);
            foreach (var sp in listSpareParts)
            {
                ListBoxSpareParts.Items.Add(sp);

            }

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            decimal netPrice = 0;
            try
            {

                Dictionary<int, (string, decimal, decimal)> sparePartsId = new Dictionary<int, (string, decimal, decimal)>();
                foreach (SparePartViewModel sparepart in ListBoxSpareParts.SelectedItems)
                {
                    sparePartsId.Add(sparepart.Id, (sparepart.Name, sparepart.Price, sparepart.Price));
                    netPrice += sparepart.Price;
                }

                WorkTypeViewModel workType = (WorkTypeViewModel)ComboBoxWorkType.SelectedItem;
                _workTypeLogic.CreateOrUpdate(new WorkTypeBindingModel
                {
                    Id = workType.Id,
                    WorkName = workType.WorkName,
                    Price = workType.Price,
                    TimeOfWorkId = workType.TimeOfWorkId,
                    WorkSpareParts = sparePartsId,
                    NetPrice = netPrice + workType.Price
                });
                MessageBox.Show("Привязка прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
                /*foreach (var sem in ListBoxSeminars.SelectedItems)
                {
                    var seminar = (SeminarViewModel)sem;
                    lunchSeminars.Add(seminar.Id, seminar.Name);
                }

                LunchViewModel lunch = (LunchViewModel)ComboBoxLunch.SelectedItem;
                _workTypeLogic.CreateOrUpdate(new LunchBindingModel
                {
                    Id = lunch.Id,
                    Name = lunch.Name,
                    Dish = lunch.Dish,
                    Drink = lunch.Drink,
                    HeadwaiterId = (int)App.Headwaiter.Id,
                    LunchSeminars = lunchSeminars
                });
                MessageBox.Show("Привязка прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();*/
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
    }
}

