using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrouwerService.DTOs
{
    public class FiliaalDTO
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int Postcode { get; set; }
        public string Woonplaats { get; set; }
    }
}
