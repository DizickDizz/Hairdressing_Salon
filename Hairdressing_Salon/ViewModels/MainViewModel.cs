using Hairdressing_Salon.Models;
using Hairdressing_Salon.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModeChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
             OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
