using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.Enum;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace GFT_ClubHouse__Management.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ClubHouse> ClubHouses { get; set; }
        public DbSet<MusicalGenre> MusicalGenres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Address>().HasData(
                new Address() {
                    Id = 1,
                    Street = "1234 1St Ave",
                    City = "Seattle",
                    State = "Washington",
                    Zip = "98101"
                });
            modelBuilder.Entity<User>().HasData(
                new User {
                    Id = 1,
                    Name = "Admin",
                    LastName = "Default",
                    Email = "admin@admin.com",
                    Password = "690e2695b6aa8f08dc1fd736072e5819",
                    Phone = "123456789",
                    Roles = UserRoles.Admin,
                    AddressId = 1
                }
            );
        }
    }
}