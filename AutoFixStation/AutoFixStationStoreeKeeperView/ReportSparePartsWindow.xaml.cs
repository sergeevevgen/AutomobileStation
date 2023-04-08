﻿using AutoFixStationContracts.BindingModels;
using AutoFixStationContracts.BusinessLogicsContracts;
using Microsoft.Win32;
using System;
using System.Windows;


namespace AutoFixStationStoreeKeeperView
{
    /// <summary>
    /// Логика взаимодействия для ReportSparePartsWindow.xaml
    /// </summary>
    public partial class ReportSparePartsWindow : Window
    {
        private readonly IStoreKeeperReportLogic _reportLogic;
        private readonly IWorkLogic _workLogic;
        //private readonly MailLogic _mailLogic;

        public ReportSparePartsWindow(IStoreKeeperReportLogic reportLogic, IWorkLogic workLogic)
        {
            InitializeComponent();
            _reportLogic = reportLogic;
            _workLogic = workLogic;
            //_mailLogic = mailLogic;
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePickerFrom.SelectedDate == null || DatePickerTo.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты начала и окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var dataSource = _reportLogic.GetTOs(new ReportBindingModel
                {
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate
                });
                LunchesGrid.ItemsSource = dataSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonPdf_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePickerFrom.SelectedDate == null || DatePickerTo.SelectedDate == null)
            {
                MessageBox.Show("Выберите даты начала и окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            {
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        _reportLogic.SaveTOsByDateToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = DatePickerFrom.SelectedDate,
                            DateTo = DatePickerTo.SelectedDate
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ButtonMail_Click(object sender, RoutedEventArgs e)
        {
            /* if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
             {
                 MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                 return;
             }
             if (DatePickerFrom.SelectedDate == null || DatePickerTo.SelectedDate == null)
             {
                 MessageBox.Show("Выберите даты начала и окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                 return;
             }
             try
             {
                 var fileName = "Отчет.pdf";

                 _reportLogic.SaveLunchesToPdf(new ReportBindingModel
                 {
                     FileName = fileName,
                     DateFrom = DatePickerFrom.SelectedDate,
                     DateTo = DatePickerTo.SelectedDate
                 }, (int)App.Headwaiter.Id);

                 _mailLogic.MailSendAsync(new MailSendInfoBindingModel
                 {
                     MailAddress = App.Headwaiter.Login,
                     Subject = "Отель 'Принцесса на горошине'",
                     Text = "Отчет по обедам от " + DatePickerFrom.SelectedDate.Value.ToShortDateString() + " по " + DatePickerTo.SelectedDate.Value.ToShortDateString(),
                     FileName = fileName
                 });
                 MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
             }*/
        }
    }
}
