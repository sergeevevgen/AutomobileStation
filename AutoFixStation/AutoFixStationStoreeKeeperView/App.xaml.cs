using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoFixStationBusinessLogic.BusinessLogics;
using AutoFixStationContracts.BusinessLogicsContracts;
using AutoFixStationContracts.StorageContracts;
using AutoFixStationContracts.ViewModels;
using AutoFixStationContracts.BindingModels;
using AutoFixStationDatabaseImplement.Implements;
using AutoFixStationBusinessLogic.OfficePackage;
using AutoFixStationBusinessLogic.OfficePackage.Implements;
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
            //currentContainer.RegisterType<IConferenceStorage, ConferenceStorage>(new HierarchicalLifetimeManager());
            //currentContainer.RegisterType<ISeminarStorage, SeminarStorage>(new HierarchicalLifetimeManager());
            //currentContainer.RegisterType<ILunchStorage, LunchStorage>(new HierarchicalLifetimeManager());
            //currentContainer.RegisterType<IRoomStorage, RoomStorage>(new HierarchicalLifetimeManager());
            //currentContainer.RegisterType<IRoomerStorage, RoomerStorage>(new HierarchicalLifetimeManager());
            /*
            currentContainer.RegisterType<IHeadwaiterLogic, HeadwaiterLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IConferenceLogic, ConferenceLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISeminarLogic, SeminarLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILunchLogic, LunchLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRoomLogic, RoomLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRoomerLogic, RoomerLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IHeadwaiterReportLogic, HeadwaiterReportLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<MailLogic>(new HierarchicalLifetimeManager());*/
            /*
            currentContainer.RegisterType<HeadwaiterAbstractSaveToPdf, HeadwaiterSaveToPdf>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<HeadwaiterAbstractSaveToExcel, HeadwaiterSaveToExcel>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<HeadwaiterAbstractSaveToWord, HeadwaiterSaveToWord>(new HierarchicalLifetimeManager());*/
            return currentContainer;
        }
    }
}