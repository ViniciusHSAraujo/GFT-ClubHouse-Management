using GFT_ClubHouse__Management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT_ClubHouse__Management.Repositories.Interfaces {
    public interface IMusicalGenreRepository : IGenericRepository<MusicalGenre>{
        List<SelectListItem> GetSelectList();
    }
}
