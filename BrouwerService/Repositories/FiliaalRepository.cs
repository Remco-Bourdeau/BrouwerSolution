using BrouwerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrouwerService.Models;
using Microsoft.EntityFrameworkCore;

namespace BrouwerService.Repositories
{
    public class FiliaalRepository:IFiliaalRepository
    {
        private readonly BierlandContext context;
        public FiliaalRepository(BierlandContext context)
        {
            this.context = context;
        }
        public async Task<List<Filiaal>> FindAllAsync() => 
            await context.Filialen.Include(filiaal => filiaal.Woonplaats).AsNoTracking().ToListAsync();

        
    }
}
