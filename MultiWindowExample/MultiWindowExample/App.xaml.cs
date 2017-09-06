using Caliburn.Micro;
using MultiWindowExample.Interfaces;
using MultiWindowExample.Services;
using MultiWindowExample.ViewModels;
using System;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace MultiWindowExample
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App
    {
        private WinRTContainer container;
        private IEventAggregator eventAggregator;
        private IWindowManagerService windowMangerService;
        public App()
        {
            InitializeComponent();
        }
        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);

            DisplayRootViewFor<MainPageViewModel>();


           

        }
        protected override void Configure()
        {
            container = new WinRTContainer();


        
            container.RegisterWinRTServices();

            container.Singleton<IWindowManagerService, WindowManagerService>();
            container.PerRequest<MainPageViewModel>();
            container.PerRequest<ChildShellViewModel>();
            container.PerRequest<CarDetailsViewModel>();
            container.PerRequest<CarsViewModel>();
          
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);

          

            return instance;
        }




        protected override Frame CreateApplicationFrame()
        {
            return base.CreateApplicationFrame();
        }
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            

            DisplayRootViewFor<MainPageViewModel>();
            windowMangerService = IoC.Get<IWindowManagerService>();
            windowMangerService.Initialize(ApplicationView.GetForCurrentView().Id);
            ApplicationView.GetForCurrentView().Consolidated += ViewConsolidated;
           
        }

        private void ViewConsolidated(ApplicationView sender, ApplicationViewConsolidatedEventArgs args)
        {
            App.Current.Exit();
        }

     

       

    }
}
