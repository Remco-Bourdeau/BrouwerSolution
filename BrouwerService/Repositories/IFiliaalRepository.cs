using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrouwerService.Models;
using System.Threading.Tasks;

namespace BrouwerService.Repositories
{
    public interface IFiliaalRepository
    {
        Task<List<Filiaal>> FindAllAsync();
    }
}
