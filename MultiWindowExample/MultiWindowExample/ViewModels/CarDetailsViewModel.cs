using System;
using Caliburn.Micro;
using MultiWindowExample.Interfaces;
using System.Linq;

namespace MultiWindowExample.ViewModels
{
    public class CarDetailsViewModel:Screen
    {
        IWindowManagerService windowService;
        public RelayCommand BackCommand { get; set; }
        public CarDetailsViewModel()
        {
            BackCommand = new RelayCommand(BackCommandExecute);
            windowService = IoC.Get<IWindowManagerService>();
        }

        private void BackCommandExecute()
        {
            var activeWindow = windowService.ActiveWindows.FirstOrDefault(x => x.Name == WindowNames.CarsWindow);
            if (activeWindow == null)
                return;
            var navigationService = IoC.Get<INavigationService>(activeWindow.NavigationServiceName);
            if (navigationService == null)
                return;

            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }

        }
    }
}
