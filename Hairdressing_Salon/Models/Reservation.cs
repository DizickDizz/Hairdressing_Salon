using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Models
{
    public class Reservation
    {
        public string UserName { get; }
        public string PhoneNumber { get; }
        public DateTime Date { get; }
        public TimeSpan Time { get; }
        public Service Service { get; }

        public Reservation(string userName, string phoneNumber, DateTime date, TimeSpan time, Service service)
        {
            UserName = userName;
            PhoneNumber = phoneNumber;
            Date = date;
            Time = time;
            Service = service;
        }
    }
}
