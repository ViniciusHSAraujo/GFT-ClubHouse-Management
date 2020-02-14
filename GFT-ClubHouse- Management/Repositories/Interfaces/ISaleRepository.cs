﻿using GFT_ClubHouse__Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFT_ClubHouse__Management.Repositories.Interfaces {
    public interface ISaleRepository : IGenericRepository<Sale>{
        
        IEnumerable<Sale> GetByUser(int id);
        
    }
}