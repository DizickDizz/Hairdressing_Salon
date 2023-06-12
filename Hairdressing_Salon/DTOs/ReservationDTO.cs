using Hairdressing_Salon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.DTOs
{
    public class ReservationDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
    }
}
