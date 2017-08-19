using MultiWindowExample.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiWindowExample.Interfaces
{
    public interface IWindowManagerService
    {

        Task ShowAsync(string childName, Type viewModelToNavigate, object parameters = null);

        List<ActiveWindow> ActiveWindows { get; }
        Task CloseAsync(string childName);
        int MainViewId { get; }

        void Initialize(int mainId);

    }
}
