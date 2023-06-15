using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Models
{
    public class Salon
    {
        private readonly ReservationBook _reservationBook;

        public Salon(ReservationBook reservationBook)
        {
            _reservationBook = reservationBook;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations() 
        {
            return await _reservationBook.GetAllReservations();
        }

        public async Task MakeReservation(Reservation reservation) 
        { 
            await _reservationBook.AddReservation(reservation);
        }
        
        public async Task DeleteReservation(Reservation reservation) 
        {
            await _reservationBook.DeleteReservation(reservation);
        }

    }
}
