using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.DbContexts
{
    public class SalonDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SalonDbContext>
    {
        public SalonDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder().UseSqlite("Data Source=salon.db").Options;

            return new SalonDbContext(options);
        }
    }
}
