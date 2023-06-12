using Hairdressing_Salon.Exceptions;
using Hairdressing_Salon.Services.ReservationConflictValidators;
using Hairdressing_Salon.Services.ReservationCreators;
using Hairdressing_Salon.Services.ReservationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Models
{
    public class ReservationBook
    {
        private readonly IReservationPrivider _reservationPrivider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator _reservationConflictValidator;

        public ReservationBook(IReservationPrivider reservationPrivider, IReservationCreator reservationCreator, IReservationConflictValidator reservationConflictValidator)
        {
            _reservationPrivider = reservationPrivider;
            _reservationCreator = reservationCreator;
            _reservationConflictValidator = reservationConflictValidator;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationPrivider.GetReservations();
        }

        public async Task AddReservation(Reservation reservation) 
        {
            Reservation conflictionReservation = await _reservationConflictValidator.GetConflictingReservation(reservation);

            if (conflictionReservation != null)
            { 
                throw new ReservationConflictException(conflictionReservation, reservation);
            }
         
            _reservationCreator.CreateReservation(reservation);
        }

        internal async Task DeleteReservation(Reservation reservation)
        {
            _reservationCreator.DeleteReservation(reservation);
        }
    }
}
