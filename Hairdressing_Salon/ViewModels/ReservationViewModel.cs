using Hairdressing_Salon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly Reservation _reservation;

        public string UserName => _reservation.UserName;
        public DateTime Date => _reservation.Date;
        public string ServiceType => _reservation.Service.Type;
        public int Price => _reservation.Service.Price;
        public TimeSpan Time => _reservation.Time;
        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }
    }
}
