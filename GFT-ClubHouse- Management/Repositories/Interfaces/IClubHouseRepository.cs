using System.Collections.Generic;
using GFT_ClubHouse__Management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GFT_ClubHouse__Management.Repositories.Interfaces {
    public interface IClubHouseRepository : IGenericRepository<ClubHouse> {
        List<SelectListItem> GetSelectList();
        List<ClubHouse> GetAllByName(string name);
    }
}