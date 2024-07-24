using System.ComponentModel.DataAnnotations;

namespace DataLayer.Data
{
    public class ServiceEntity
    {
        public int ServiceId { get; set; }

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
