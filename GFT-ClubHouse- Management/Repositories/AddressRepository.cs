using System.Linq;
using GFT_ClubHouse__Management.Data;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Repositories.Interfaces;

namespace GFT_ClubHouse__Management.Repositories {
    public class AddressRepository : IAddressRepository {
        private readonly ApplicationDbContext _dbContext;

        public AddressRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public bool Exists(int id) {
            return _dbContext.Set<Address>().Any(x => x.Id.Equals(id));
        }
    }
}