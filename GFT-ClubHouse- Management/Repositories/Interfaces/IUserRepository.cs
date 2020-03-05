using System.Collections.Generic;
using GFT_ClubHouse__Management.Models;
using GFT_ClubHouse__Management.Models.Enum;
using GFT_ClubHouse__Management.Models.ViewModels.API.UserViewModels;

namespace GFT_ClubHouse__Management.Repositories.Interfaces {
    public interface IUserRepository : IGenericRepository<User> {
        User Login(string email, string pass, UserRoles roles);

        List<UserListViewModel> GetAllSimplified();
        UserListViewModel GetSimplified(int id);

        bool IsRegistered(string email, UserRoles roles);
    }
}