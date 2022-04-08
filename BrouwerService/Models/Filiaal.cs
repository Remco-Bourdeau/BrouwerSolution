using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrouwerService.Models
{
    public class Filiaal
    {
        public int Id { get; set; }
        public string Naam { get; set;}
        public int HuurPrijs { get; set; }
        public int WoonPlaatsId { get; set; }
        public Woonplaats Woonplaats { get; set; }
    }
}
