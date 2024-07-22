using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class BookingServiceEntity
    {
        public int Id { get; set; }
        public int IdPrenotazione { get; set; }
        public int IdServizio { get; set; }

        [Display(Name = "Data"), Required(ErrorMessage = "Inserisci la data")]
        public DateOnly Date { get; set; }

        [Display(Name = "Quantità"), Required(ErrorMessage = "Inserisci la quantità")]
        public int Amount { get; set; }

        [Display(Name = "Prezzo"),
            Required(ErrorMessage = "Inserisci il prezzo")]
        public decimal Price { get; set; }
    }
}
