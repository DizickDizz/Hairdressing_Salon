using Hairdressing_Salon.Models;
using Hairdressing_Salon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Stores
{
    public class SalonStore
    {
        private List<Reservation> _reservation;
        public IEnumerable<Reservation> Reservations => _reservation;
        private readonly Salon _salon;
        private readonly Lazy<Task> _initializeLazy;

        public event Action<Reservation> ReservationDelete;

        public SalonStore(Salon salon)
        {
            _reservation = new();
            _salon = salon;
            _initializeLazy = new(Initialize());
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }
        
        public async Task MakeReservation(Reservation reservation)
        {
            await _salon.MakeReservation(reservation);

            _reservation.Add(reservation);
        }
        public async Task DeleteReservation(ReservationListingViewModel viewModel)
        {
            var reservation = new Reservation(viewModel.SelectedReservation.UserName, "", viewModel.SelectedReservation.Date,
                viewModel.SelectedReservation.Time, new Service(viewModel.SelectedReservation.ServiceType, viewModel.SelectedReservation.Price));
            await _salon.DeleteReservation(reservation);

            var itemToDelete = _reservation.Where(r => r.UserName == reservation.UserName && r.Date == reservation.Date && r.Time == reservation.Time).FirstOrDefault();
            _reservation.Remove(itemToDelete);

            OnReservationDelete(reservation);
        }

        private void OnReservationDelete(Reservation reservation)
        {
            ReservationDelete?.Invoke(reservation);
        }

        private async Task Initialize()
        {
            IEnumerable<Reservation> reservations = await _salon.GetAllReservations();

            _reservation.Clear();
            _reservation.AddRange(reservations);
        }
    }
}
