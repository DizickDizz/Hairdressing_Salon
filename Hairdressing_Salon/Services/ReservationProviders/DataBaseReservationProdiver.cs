using Hairdressing_Salon.DbContexts;
using Hairdressing_Salon.DTOs;
using Hairdressing_Salon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Services.ReservationProviders
{
    public class DataBaseReservationProdiver : IReservationPrivider
    {
        private readonly SalonDBContextFactory _dbContextFactory;

        public DataBaseReservationProdiver(SalonDBContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            using (SalonDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ReservationDTO> reservationDTOs = await context.Reservations.ToListAsync();

                return reservationDTOs.Select(r => new Reservation(
                    r.UserName, 
                    r.PhoneNumber, 
                    r.Date,
                    r.Time,
                    new Service(r.Type, r.Price)));
            }
        }
    }
}
