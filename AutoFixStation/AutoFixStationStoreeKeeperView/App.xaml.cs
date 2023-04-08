﻿using AutoFixStationBusinessLogic;
using AutoFixStationBusinessLogic.BusinessLogics;
using AutoFixStationBusinessLogic.OfficePackage.Implements;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.StorageContracts;
using AutoFixStationContracts.ViewModels;
using AutoFixStationDatabaseImplement.Implements;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace AutoFixStationStoreeKeeperView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static IUnityContainer container = null;


        public static StoreKeeperViewModel StoreKeeper { get; set; }
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            /*var mailSender = Container.Resolve<MailLogic>();
            mailSender.MailConfig(new MailConfigBindingModel
            {
                MailLogin = ConfigurationManager.AppSettings["MailLogin"],
                MailPassword = ConfigurationManager.AppSettings["MailPassword"],
                SmtpClientHost = ConfigurationManager.AppSettings["SmtpClientHost"],
                SmtpClientPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]),
                PopHost = ConfigurationManager.AppSettings["PopHost"],
                PopPort = Convert.ToInt32(ConfigurationManager.AppSettings["PopPort"])
            });*/

            var authorizationWindow = Container.Resolve<AuthorizationWindow>();
            authorizationWindow.ShowDialog();
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IStoreKeeperStorage, StoreKeeperStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITimeOfWorkStorage, TimeOfWorkStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISparePartStorage, SparePartStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkTypeStorage, WorkTypeStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkStorage, WorkStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITOStorage, TOStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IServiceRecordStorage, ServiceRecordStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IEmployeeStorage, EmployeeStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICarStorage, CarStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IStoreKeeperLogic, StoreKeeperLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITimeOfWorkLogic, TimeOfWorkLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISparePartLogic, SparePartLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkTypeLogic, WorkTypeLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkLogic, WorkLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITOLogic, TOLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IServiceRecordLogic, ServiceRecordLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IEmployeeLogic, EmployeeLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICarLogic, CarLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStoreKeeperReportLogic, StoreKeeperReportLogic>(new HierarchicalLifetimeManager());
            //currentContainer.RegisterType<MailLogic>(new HierarchicalLifetimeManager());*/

            currentContainer.RegisterType<AbstractSaveToPdf, SaveToPdf>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToExcel, SaveToExcel>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToWord, SaveToWord>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}