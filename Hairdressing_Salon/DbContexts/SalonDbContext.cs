using Hairdressing_Salon.DTOs;
using Hairdressing_Salon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.DbContexts
{
    public class SalonDbContext : DbContext
    {
        public SalonDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ReservationDTO> Reservations { get; set; }
    }
}
