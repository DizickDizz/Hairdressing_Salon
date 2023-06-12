using Hairdressing_Salon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Services.ReservationCreators
{
    public interface IReservationCreator
    {
        Task CreateReservation(Reservation reservation);
        Task DeleteReservation(Reservation reservation);
    }
}
