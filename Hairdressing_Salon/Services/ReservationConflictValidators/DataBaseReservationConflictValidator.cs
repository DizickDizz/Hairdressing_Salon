using Hairdressing_Salon.DbContexts;
using Hairdressing_Salon.DTOs;
using Hairdressing_Salon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.Services.ReservationConflictValidators
{
    public class DataBaseReservationConflictValidator : IReservationConflictValidator
    {
        private readonly SalonDBContextFactory _dbContextFactory;

        public DataBaseReservationConflictValidator(SalonDBContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<Reservation> GetConflictingReservation(Reservation reservation)
        {
            using (SalonDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = await context.Reservations.
                     Where(r => r.Date == reservation.Date && r.Time == reservation.Time).
                     FirstOrDefaultAsync();
                
                if (reservationDTO == null) { return null; }
                return ToReservation(reservationDTO);
            }
        }

        public static Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(dto.UserName, dto.PhoneNumber, dto.Date, dto.Time, new Service(dto.Type, dto.Price));
        }
    }
}
