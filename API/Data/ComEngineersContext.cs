using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComEngineers.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ComEngineers.API.Data
{
    public class ComEngineersContext : DbContext
    {
        public DbSet<Session> Session { get; set; }
        public DbSet<Accelerometer> Accelerometer { get; set; }
        public DbSet<Gyroscope> Gyroscope { get; set; }
        public DbSet<HeartRate> HeartRate { get; set; }
        public DbSet<Temperature> Temperature { get; set; }
        public DbSet<TypesOfData> TypesOfData{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = @"Data Source=localhost;Initial Catalog=biometric_storage;User ID=sa;Password=Psmyth2024;Encrypt=False";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
