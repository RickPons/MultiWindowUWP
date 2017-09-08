using Caliburn.Micro;
using MultiWindowExample.Interfaces;
using MultiWindowExample.Models;

namespace MultiWindowExample.ViewModels
{
    public class MainPageViewModel:Screen
    {
        private IBingSearchService bingService;
        public RelayCommand ShowCarsCommand { get; set; }
        public RelayCommand ShowBuildingsCommand { get; set; }
        public RelayCommand ShowPyramidsCommand { get; set; }

        IWindowManagerService windowService = null;

        public MainPageViewModel()
        {
            bingService = IoC.Get<IBingSearchService>();
            windowService = IoC.Get<IWindowManagerService>();
            ShowCarsCommand = new RelayCommand(ShowCarsCommandExecute);
            ShowBuildingsCommand = new RelayCommand(ShowBuildingsCommandExecute);
            ShowPyramidsCommand = new RelayCommand(ShowPyramidsCommandExecute);
        }

        private void ShowPyramidsCommandExecute()
        {
            windowService.ShowAsync(WindowNames.PyramidsWindow, typeof(ViewModels.ImagesViewModel),
                new ImageParameters()
                {
                    Keyword = "pyramids",
                    ViewTitle = "Pyramids View",
                    WindowName= WindowNames.PyramidsWindow
                });
        }

        private void ShowBuildingsCommandExecute()
        {
            windowService.ShowAsync(WindowNames.BuildingsWindow, typeof(ViewModels.ImagesViewModel),
               new ImageParameters()
               {
                   Keyword = "buildings",
                   ViewTitle = "Buildings View",
                   WindowName = WindowNames.BuildingsWindow
               });
        }

        private void ShowCarsCommandExecute()
        {
            windowService.ShowAsync(WindowNames.CarsWindow, typeof(ViewModels.ImagesViewModel),
                new ImageParameters()
                {
                     Keyword="sport cars",
                     ViewTitle="Sport Cars View",
                     WindowName = WindowNames.CarsWindow
                });

        }
        
    }
}
