using Caliburn.Micro;
using MultiWindowExample.Interfaces;
using System.Linq;

namespace MultiWindowExample.ViewModels
{
    public class ImageDetailsViewModel:Screen
    {

        public object Parameter { get; set; }
        IWindowManagerService windowService;
        public RelayCommand BackCommand { get; set; }
        public ImageDetailsViewModel()
        {
            BackCommand = new RelayCommand(BackCommandExecute);
            windowService = IoC.Get<IWindowManagerService>();
        }


        private string imageUrl;

        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                imageUrl = value;
                NotifyOfPropertyChange();
            }
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

        protected override void OnActivate()
        {
            base.OnActivate();
            var img = Parameter as MultiWindowExample.Models.Image;
            if (img != null)
            {
                ImageUrl = img.contentUrl;
            }
        }
    }
}
