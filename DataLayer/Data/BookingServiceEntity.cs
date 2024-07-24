using System.ComponentModel.DataAnnotations;

namespace DataLayer.Data
{
    public class BookingServiceEntity
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int ServiceId { get; set; }

        [Display(Name = "Data"), Required(ErrorMessage = "Inserisci la data")]
        public DateTime Date { get; set; }

        [Display(Name = "Quantità"), Required(ErrorMessage = "Inserisci la quantità")]
        public int Quantity { get; set; }

        [Display(Name = "Prezzo"),
            Required(ErrorMessage = "Inserisci il prezzo")]
        public decimal Price { get; set; }
    }
}
