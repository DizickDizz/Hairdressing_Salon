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
    public class ReservationListingViewModel : ViewModelBase
    {

        private ObservableCollection<ReservationViewModel> _reservation;
        private readonly SalonStore _salonStore;
        private ReservationViewModel _selectedReservation;

        public ReservationViewModel SelectedReservation
        {
            get
            {
                return _selectedReservation;
            }
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }

        public IEnumerable<ReservationViewModel> Reservations 
        { 
            get { return _reservation; }
        }
        public ICommand LoadReservationCommand { get; } 
        public ICommand MakeReservationCommand { get; }
        public ICommand DeleteReservationCommand { get; }
        public ICommand ReservationHistoryCommand { get; }

        public ReservationListingViewModel(SalonStore salonStore, NavigationService<MakeReservationViewModel> MakeReservationNavigationService, NavigationService<ReservationHistoryViewModel> ReservationHistoryNavigationService)
        {
            _reservation = new ObservableCollection<ReservationViewModel>();
            _salonStore = salonStore;
            MakeReservationCommand = new NavigateCommand<MakeReservationViewModel>(MakeReservationNavigationService);
            ReservationHistoryCommand = new NavigateCommand<ReservationHistoryViewModel>(ReservationHistoryNavigationService);
            LoadReservationCommand = new LoadReservationCommand(salonStore, this);
            DeleteReservationCommand = new DeleteReservationCommand(salonStore, this);

            _salonStore.ReservationDelete += OnReservationDelete;
        }

        public override void Dispose()
        {
            _salonStore.ReservationDelete -= OnReservationDelete;
            base.Dispose();
        }

        private void OnReservationDelete(Reservation reservation)
        {
            var itemToDelete = _reservation.Where(r => r.UserName == reservation.UserName && r.Date == reservation.Date && r.Time ==  reservation.Time).FirstOrDefault(); 
            _reservation.Remove(itemToDelete);              
        }

        public static ReservationListingViewModel LoadViewModel(
            SalonStore salonStore, 
            NavigationService<MakeReservationViewModel> MakeReservationNavigationService,
            NavigationService<ReservationHistoryViewModel> ReservationHistoryNavigationService)
        {
            ReservationListingViewModel viewModel = new ReservationListingViewModel(salonStore, MakeReservationNavigationService, ReservationHistoryNavigationService);

            viewModel.LoadReservationCommand.Execute(null);

            return viewModel;
        }
        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservation.Clear();

            foreach (Reservation reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                var DateCombineWithTime = reservationViewModel.Date.Add(reservationViewModel.Time);

                if (DateCombineWithTime >  DateTime.Now) 
                    _reservation.Add(reservationViewModel);
            }
        }
    }
}
