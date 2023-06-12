using Hairdressing_Salon.DbContexts;
using Hairdressing_Salon.DTOs;
using Hairdressing_Salon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Services.ReservationCreators
{
    public class DataBaseReservationCreater : IReservationCreator
    {
        private readonly SalonDBContextFactory _dbContextFactory;

        public DataBaseReservationCreater(SalonDBContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task CreateReservation(Reservation reservation)
        {
            using (SalonDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = ToReservationDTO(reservation);
                context.Reservations.Add(reservationDTO);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteReservation(Reservation reservation)
        {
            using (SalonDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationToDelete = await context.Reservations.FirstOrDefaultAsync(r => r.UserName == reservation.UserName && 
                r.Date == reservation.Date && r.Time == reservation.Time);

                context.Reservations.Remove(reservationToDelete);
                await context.SaveChangesAsync();
            }
        }

        private ReservationDTO ToReservationDTO(Reservation reservation)
        {
            return new ReservationDTO()
            {
                UserName = reservation.UserName,
                PhoneNumber = reservation.PhoneNumber,
                Date = reservation.Date,
                Time = reservation.Time,
                Type = reservation.Service.Type,
                Price = reservation.Service.Price
            };
        }
    }
}
