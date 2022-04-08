using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrouwerService.Models
{
    public class Woonplaats
    {
        public int Id { get; set; }
        public int Postcode { get; set; }
        public string Naam { get; set; }
        public List<Filiaal> Filialen { get; set; }
    }
}
