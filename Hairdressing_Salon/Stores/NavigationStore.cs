using Hairdressing_Salon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Stores
{
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentVeiewModelChanged();
            }
        }

        private void OnCurrentVeiewModelChanged()
        {
            CurrentViewModeChanged?.Invoke();
        }

        public event Action CurrentViewModeChanged;
    }
}
