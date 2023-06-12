using Hairdressing_Salon.Models;
using Hairdressing_Salon.Stores;
using Hairdressing_Salon.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Commands
{
    public class DeleteReservationCommand : AsyncCommandBase
    {
        private readonly SalonStore _salonStore;
        private readonly ReservationListingViewModel _viewModel;

        public DeleteReservationCommand(SalonStore salonStore, ReservationListingViewModel reservationListingViewModel)
        {
            _salonStore = salonStore;
            _viewModel = reservationListingViewModel;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public Reservation ReservationConvert(ReservationListingViewModel viewModel)
        {
            return new Reservation(viewModel.SelectedReservation.UserName, "", viewModel.SelectedReservation.Date, 
                viewModel.SelectedReservation.Time, new Service(viewModel.SelectedReservation.ServiceType, viewModel.SelectedReservation.Price));
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _salonStore.DeleteReservation(_viewModel);
        }
    }
}
