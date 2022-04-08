using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BrouwerService.Models
{
    public class BierlandContext:DbContext
    {
        public BierlandContext(DbContextOptions<BierlandContext> options) : base(options) { }
        public DbSet<Brouwer> Brouwers { get; set; }
        public DbSet<Woonplaats> Woonplaatsen { get; set; }
        public DbSet<Filiaal> Filialen { get; set; }
    }
}
