using Microsoft.EntityFrameworkCore;
using System;

namespace ImageShareLikesEF.Data
{
    public class ImageDbContext : DbContext
    {
        private string _connectionString;

        public ImageDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Image> Images { get; set; }
    }
}