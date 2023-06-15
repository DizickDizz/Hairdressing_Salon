using Hairdressing_Salon.Commands;
using Hairdressing_Salon.Models;
using Hairdressing_Salon.Services;
using Hairdressing_Salon.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hairdressing_Salon.ViewModels
{
    public class ReservationHistoryViewModel : ViewModelBase
    {
        private ObservableCollection<ReservationViewModel> _reservation;
        private readonly SalonStore _salonStore;
        public ICommand BackCommand { get; }
        public IEnumerable<ReservationViewModel> Reservations
        {
            get { return _reservation; }
        }
        public ReservationHistoryViewModel(SalonStore salonStore, NavigationService<ReservationListingViewModel> reservationViewNavigationService)
        {
            _salonStore = salonStore;
            _reservation = new ObservableCollection<ReservationViewModel>();
            BackCommand = new NavigateCommand<ReservationListingViewModel>(reservationViewNavigationService);
            UpdateReservations(_salonStore.Reservations);
        }
        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservation.Clear();

            foreach (Reservation reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservation.Add(reservationViewModel);
            }
        }
    }
}
