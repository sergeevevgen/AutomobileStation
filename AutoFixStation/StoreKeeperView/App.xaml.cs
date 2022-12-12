using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity.Lifetime;
using Unity;
using AutoFixStationContracts.ViewModels;
using AutoFixStationContracts.StorageContracts;
using AutoFixStationDatabaseImplement.Implements;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationBusinessLogic.BusinessLogics;

namespace StoreKeeperView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer container = null;
        public static StoreKeeperViewModel Headwaiter { get; set; }
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

            var authorizationWindow = Container.Resolve<SignInWindow>();
            authorizationWindow.ShowDialog();
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IStoreKeeperStorage, StoreKeeperStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITOStorage, TOStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICarStorage, CarStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISparePartStorage, SparePartStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkStorage, WorkStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITimeOfWorkStorage, TimeOfWorkStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IStoreKeeperLogic, StoreKeeperLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITOLogic, TOLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICarLogic, CarLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISparePartLogic, SparePartLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkLogic, WorkLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITimeOfWorkLogic, TimeOfWorkLogic>(new HierarchicalLifetimeManager());
            //currentContainer.RegisterType<IHeadwaiterReportLogic, HeadwaiterReportLogic>(new HierarchicalLifetimeManager());
            //currentContainer.RegisterType<MailLogic>(new HierarchicalLifetimeManager());

            //currentContainer.RegisterType<HeadwaiterAbstractSaveToPdf, HeadwaiterSaveToPdf>(new HierarchicalLifetimeManager());
            //currentContainer.RegisterType<HeadwaiterAbstractSaveToExcel, HeadwaiterSaveToExcel>(new HierarchicalLifetimeManager());
            //currentContainer.RegisterType<HeadwaiterAbstractSaveToWord, HeadwaiterSaveToWord>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
