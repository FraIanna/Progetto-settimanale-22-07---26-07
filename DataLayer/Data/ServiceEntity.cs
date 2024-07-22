using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class ServiceEntity
    {
        public int Id { get; set; }

        [
            Display(Name = "Descrizione"),
            Required(ErrorMessage = "Inserisci la descrizione", AllowEmptyStrings = false), 
            StringLength(200)
        ]
        public string Description { get; set; }

        [Display(Name = "Prezzo"), Required]
        public decimal Price { get; set; }
    }
}
