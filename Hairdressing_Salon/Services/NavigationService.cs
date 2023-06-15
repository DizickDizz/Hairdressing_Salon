using Hairdressing_Salon.Commands;
using Hairdressing_Salon.Stores;
using Hairdressing_Salon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Services
{
    public class NavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
