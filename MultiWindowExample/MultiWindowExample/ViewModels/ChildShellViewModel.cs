using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace MultiWindowExample.ViewModels
{
    public class ChildShellViewModel:ChildViewBase
    {
        private readonly WinRTContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private INavigationService navigationService;
        private bool _resume;

        public ChildShellViewModel(WinRTContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;

        }

        public override async void SetupNavigationService(Frame frame, object parameters = null)
        {
            this.WindowId = System.Convert.ToInt32(WindowId);

            var navigationServiceName = "NavigationService_" + Id;
            ServiceName = navigationServiceName;
            if (_container.HasHandler(typeof(INavigationService), navigationServiceName))
                _container.UnregisterHandler(typeof(INavigationService), navigationServiceName);

            _container.RegisterInstance(typeof(INavigationService), navigationServiceName, new FrameAdapter(frame));
            navigationService = IoC.Get<INavigationService>(navigationServiceName);
           
           
            await Task.Delay(500);
            var result = navigationService.NavigateToViewModel(RootViewModel, parameters);

            if (_resume)
                navigationService.ResumeState();
        }

     
    }
}
