using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;


namespace BrouwersWebClient.Models
{
    [DataContract(Name ="brouwer", Namespace ="")]
    public class Brouwer
    {
        [DataMember(Name ="id", Order =1)]
        public int Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength =1)]
        [DataMember(Name ="naam", Order =2)]
        public string Naam { get; set; }
        [Range(1000, 9999)]
        [DataMember(Name ="postcode", Order =3)]
        public int Postcode { get; set; }
        [Required]
        [StringLength(255, MinimumLength =1)]
        [DataMember(Name ="gemeente", Order =4)]
        public string Gemeente { get; set; }
    }
}
