using GFT_ClubHouse__Management.Models;
using Microsoft.EntityFrameworkCore;

namespace GFT_ClubHouse__Management.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<HouseClub> HouseClubs { get; set; }
        public DbSet<MusicalGenre> MusicalGenres { get; set; }
        public DbSet<User> Users { get; set; }
    }
}