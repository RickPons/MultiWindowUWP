using Caliburn.Micro;
using MultiWindowExample.Interfaces;
using MultiWindowExample.Models;
using MultiWindowExample.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MultiWindowExample.Services
{
    public class WindowManagerService : IWindowManagerService
    {

        public WindowManagerService()
        {

        }

        private int mainId;
        private List<ActiveWindow> activeWindows;
        public List<ActiveWindow> ActiveWindows
        {
            get
            {
                if (activeWindows == null)
                    return activeWindows = new List<ActiveWindow>();
                return activeWindows;
            }
            protected set
            {
                activeWindows = value;
            }
        }

        public int MainViewId => mainId;

        public async Task CloseAsync(string childName)
        {
            var window = ActiveWindows.FirstOrDefault(x => x.Name == childName);
            if (window != null)
            {

                await window.viewControl.Dispatcher.TryRunAsync(CoreDispatcherPriority.Low, () =>
                {
                    removeWindow(window.viewControl);
                    window.viewControl.Close();

                });

            }

        }

        public void Initialize(int _mainId)
        {
            mainId = _mainId;
        }

        public async Task ShowAsync(string childName, Type viewModelToNavigate, object parameters = null)
        {

            var activeWindow = ActiveWindows.FirstOrDefault(x => x.Name == childName);
            if (activeWindow == null)
            {
                CoreDispatcher dispather = null;
                ViewLifetimeControl viewControl = null;
                await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    viewControl = ViewLifetimeControl.CreateForCurrentView();
                    viewControl.Title = childName;
                    dispather = viewControl.Dispatcher;
                    viewControl.Released += ViewControl_Released;
                    viewControl?.StartViewInUse();
                    Window.Current.Content = new ChildShellView();
                    ((ChildShellView)Window.Current.Content).SetupViewName(childName, viewControl);
                    ((ChildShellView)Window.Current.Content).SetupNavigationService(viewModelToNavigate, parameters);

                    int newViewId = 0;
                    newViewId = ApplicationView.GetForCurrentView().Id;
                    var navigationServiceName = "NavigationService_" + childName;
                    ActiveWindows.Add(new ActiveWindow()
                    {
                        Name = childName,
                        NavigationServiceName = navigationServiceName,
                        Id = newViewId,
                        viewControl = viewControl
                    });
                    var navigationService = IoC.Get<INavigationService>(navigationServiceName);
                    navigationService.BackRequested += NavigationService_BackRequested;
                    navigationService.Navigated += NavigationService_Navigated;
                    Window.Current.Activate();



                });
                viewControl.StartViewInUse();

                bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewControl.Id);

                if (!viewShown)
                {
                    Debug.WriteLine("La vista no se ha podido crear");
                }

                viewControl.StopViewInUse();

            }
            else
            {
                var navigationService = IoC.Get<INavigationService>(activeWindow.NavigationServiceName);
                if (navigationService == null)
                    return;
               await activeWindow.viewControl.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => 
                {
                    var currentSource = navigationService.SourcePageType;
                    navigationService.NavigateToViewModel(viewModelToNavigate, parameters);
                    await ApplicationViewSwitcher.SwitchAsync(activeWindow.Id, mainId, ApplicationViewSwitchingOptions.Default);
                    
                });
              
            }
           

        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            var frame = sender as Frame;
            if (frame == null)
                return;
            if (frame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            }
        }

        private void NavigationService_BackRequested(object sender, BackRequestedEventArgs e)
        {
            var frame = sender as FrameAdapter;
            if (frame == null)
                return;
            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        private void ViewControl_Released(object sender, EventArgs e)
        {
            removeWindow(sender);

        }

        private void removeWindow(object sender)
        {
            var viewControl = sender as ViewLifetimeControl;
            var activeWindow = ActiveWindows.FirstOrDefault(x => x.Id == viewControl.Id);
            if (activeWindow != null)
            {
                ActiveWindows.Remove(activeWindow);
                var navigationService = IoC.Get<INavigationService>(activeWindow.NavigationServiceName);
                navigationService.BackRequested -= NavigationService_BackRequested;
                navigationService.Navigated -= NavigationService_Navigated;
                var container = IoC.Get<WinRTContainer>();
                if (container.HasHandler(typeof(INavigationService), activeWindow.NavigationServiceName))
                    container.UnregisterHandler(typeof(INavigationService), activeWindow.NavigationServiceName);
            }

            if (viewControl == null)
                return;
            viewControl.Released -= ViewControl_Released;
            viewControl = null;
        }
    }
}
