using GFT_ClubHouse__Management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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