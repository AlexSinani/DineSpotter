using DineSpotterRestaurantManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DineSpotterRestaurantManagement.Context
{
    public class appDbContext : DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Restaurant> restaurants { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Restaurant>().ToTable("Restaurant");
            builder.Entity<User>().ToTable("User");
            builder.Entity<Reservation>().ToTable("Reservation");
        }
    }
}
