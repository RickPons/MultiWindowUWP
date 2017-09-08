using System;
using Caliburn.Micro;
using System.Linq;
using MultiWindowExample.Interfaces;
using System.Collections.ObjectModel;
using MultiWindowExample.Models;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace MultiWindowExample.ViewModels
{
    public class ImagesViewModel:Screen
    {

        IWindowManagerService windowService;
        IBingSearchService bingService;
        public RelayCommand<object> ShowCarDetailsCommand { get; set; }
        public object Parameter { get; set; }
        private string viewName;

        private ObservableCollection<Models.Image> images;
        public ObservableCollection<Models.Image> Images
        {
            get
            {
                return images;
            }
            set
            {
                images = value;
                NotifyOfPropertyChange();
            }
        }

        public ImagesViewModel()
        {
            bingService = IoC.Get<IBingSearchService>();
            windowService = IoC.Get<IWindowManagerService>();
            ShowCarDetailsCommand = new RelayCommand<object>(ShowCarDetailsCommandExecute);
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            try
            {
                var parameters = Parameter as ImageParameters;
                if (parameters == null)
                {
                    return;
                }
                DisplayName = parameters.ViewTitle;
                viewName = parameters.WindowName;
                var result = await bingService.GetImagesAsync(parameters.Keyword);
                if(result!=null && result.value != null)
                {
                    Images = new ObservableCollection<Models.Image>(result.value);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }

        private async void ShowCarDetailsCommandExecute(object obj)
        {
            var item = obj as ItemClickEventArgs;
            if (item == null)
                return;

            
            var activeWindow = windowService.ActiveWindows.FirstOrDefault(x => x.Name == viewName);
            if (activeWindow == null)
                return;
           

           

            await  activeWindow.viewControl.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, 
                () => 
                {
                    var navigationService = IoC.Get<INavigationService>(activeWindow.NavigationServiceName);
                    if (navigationService == null)
                        return;
                    navigationService.NavigateToViewModel<ImageDetailsViewModel>(item.ClickedItem);

                });
        }
    }
}
