using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BrouwersClient
{
    [DataContract(Name ="brouwer", Namespace ="")]
    public class Brouwer
    {
        [DataMember(Name ="id", Order =1)]
        public int ID { get; set; }
        [DataMember(Name ="naam", Order =2)]
        public string Naam { get; set; }
        [DataMember(Name ="Postcode", Order =3)]
        public int Postcode { get; set; }
        [DataMember(Name ="gemeente", Order =4)]
        public string Gemeente { get; set; }

    }
}
