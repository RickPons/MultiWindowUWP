using System;
using Caliburn.Micro;
using MultiWindowExample.Interfaces;

namespace MultiWindowExample.ViewModels
{
    public class MainPageViewModel:Screen
    {
       public RelayCommand ShowCarsCommand { get; set; }
        IWindowManagerService windowService = null;

        public MainPageViewModel()
        {
            windowService = IoC.Get<IWindowManagerService>();
            ShowCarsCommand = new RelayCommand(ShowCarsCommandExecute);
        }

        private void ShowCarsCommandExecute()
        {
            windowService.ShowAsync(WindowNames.CarsWindow, typeof(ViewModels.CarsViewModel));

        }
    }
}
