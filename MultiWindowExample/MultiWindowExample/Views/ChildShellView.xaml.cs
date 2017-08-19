using Caliburn.Micro;
using MultiWindowExample.Interfaces;
using MultiWindowExample.ViewModels;
using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MultiWindowExample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChildShellView : Page
    {

        public ViewLifetimeControl thisViewControl;
        public string Name;
        int mainViewId;

        IWindowManagerService windowManagerService = null;
        public ChildShellView()
        {
            InitializeComponent();
            windowManagerService = IoC.Get<IWindowManagerService>();


            Loaded += ChildShellView_Loaded;

        }

        private void ChildShellView_Loaded(object sender, RoutedEventArgs e)
        {

            mainViewId = windowManagerService.MainViewId;
            thisViewControl.Released += ViewLifetimeControl_Released;
        }

        private void ViewLifetimeControl_Released(object sender, EventArgs e)
        {
            Loaded -= ChildShellView_Loaded;
            ((ViewLifetimeControl)sender).Released -= ViewLifetimeControl_Released;
            DataContext = null;
            var element = frame.Content as FrameworkElement;
            if (element != null)
            {

                var deactivator = element.DataContext as IDeactivate;
                if (deactivator != null)
                {
                    deactivator.Deactivate(true);
                }
                deactivator = null;
            }
            var disposable = element.DataContext as IDisposable;
            disposable?.Dispose();
            disposable = null;
            element = null;
            frame.Content = null;
            frame = null;
            GC.Collect();


            Window.Current.Close();
        }




        public void SetupViewName(string childName, ViewLifetimeControl control)
        {
            this.DataContext = IoC.Get<ChildShellViewModel>();
            thisViewControl = control;
            var id = ApplicationView.GetForCurrentView().Id;

            var shellVM = DataContext as ChildShellViewModel;
            shellVM.Id = childName;
            shellVM.WindowId = id;

        }

        public void SetupNavigationService(Type ViewModelToNavigate, object parameters = null)
        {
            var shellVM = this.DataContext as ChildShellViewModel;

            shellVM.RootViewModel = ViewModelToNavigate;
            shellVM?.SetupNavigationService(frame, parameters);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
    }
}
