using System;
using Windows.UI.Xaml.Controls;

namespace MultiWindowExample.Interfaces
{
    public interface IChildView
    {
        string ServiceName { get; set; }
        int WindowId { get; set; }
        string Id { get; set; }
        Type RootViewModel { get; set; }
        void SetupNavigationService(Frame frame, object parameters = null);
    }
}
