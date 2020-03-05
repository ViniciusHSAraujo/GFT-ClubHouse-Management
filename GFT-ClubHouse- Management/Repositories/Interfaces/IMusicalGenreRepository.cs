using System.Collections.Generic;
using GFT_ClubHouse__Management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GFT_ClubHouse__Management.Repositories.Interfaces {
    public interface IMusicalGenreRepository : IGenericRepository<MusicalGenre> {
        List<SelectListItem> GetSelectList();
        List<MusicalGenre> GetAllByName(string name);
    }
}