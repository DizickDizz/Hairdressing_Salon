using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairdressing_Salon.DbContexts
{
    public class SalonDBContextFactory
    {
        private readonly string _connectionString;

        public SalonDBContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SalonDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new SalonDbContext(options);
        }
    }
}
