﻿using Hairdressing_Salon.Services;
using Hairdressing_Salon.Stores;
using Hairdressing_Salon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel> _navigationService;

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
