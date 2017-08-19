using System;
using Caliburn.Micro;
using System.Linq;
using MultiWindowExample.Interfaces;

namespace MultiWindowExample.ViewModels
{
    public class CarsViewModel:Screen
    {

        IWindowManagerService windowService;

        public RelayCommand ShowCarDetailsCommand { get; set; }
        public CarsViewModel()
        {
            windowService = IoC.Get<IWindowManagerService>();
            ShowCarDetailsCommand = new RelayCommand(ShowCarDetailsCommandExecute);
        }

        private void ShowCarDetailsCommandExecute()
        {
            var activeWindow = windowService.ActiveWindows.FirstOrDefault(x => x.Name == WindowNames.CarsWindow);
            if (activeWindow == null)
                return;
            var navigationService = IoC.Get<INavigationService>(activeWindow.NavigationServiceName);
            if (navigationService == null)
                return;
            navigationService.NavigateToViewModel<CarDetailsViewModel>();
        }
    }
}
