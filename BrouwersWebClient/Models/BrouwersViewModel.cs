using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrouwersWebClient.Models
{
    public class BrouwersViewModel
    {
        public List<Brouwer> Brouwers { get; set; }
        public Brouwer GeselecteerdeBrouwer { get; set; }
    }
}
