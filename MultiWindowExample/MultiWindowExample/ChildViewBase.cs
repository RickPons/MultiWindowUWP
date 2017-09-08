using Caliburn.Micro;
using MultiWindowExample.Interfaces;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace MultiWindowExample
{
    public class ChildViewBase : Screen, IChildView
    {
        INavigationService navigationService;
        public int WindowId { get; set; }
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public Type RootViewModel { get; set; }

      
        public virtual void SetupNavigationService(Frame frame, object parameters)
        {
            throw new NotImplementedException();
        }

      

    }
}
