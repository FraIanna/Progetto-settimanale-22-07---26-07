using System.ComponentModel.DataAnnotations;

namespace DataLayer.Data
{
    public class BookingEntity
    {
        public int BookingId { get; set; }
        [Display (Name = "Codice Fiscale del Cliente")]
        [Required(ErrorMessage = "Inserisci il codice fiscale", AllowEmptyStrings = false)]
        [StringLength(16, MinimumLength = 16)]
        public string ClientFiscalCode { get; set; }

        [   
            Display (Name = "Numero Camera"),
            Required(ErrorMessage = "Inserisci il numero della camera")
        ]
        public int RoomNumber { get; set; }

        [
            Display(Name = "Data di prenotazione"),
            Required(ErrorMessage = "Inserisci la data della prenotazione")
        ]
        public DateTime BookingDate { get; set; }

        [
            Display(Name = "Numero Progeressivo anno"),
            Required(ErrorMessage = "Inserisci il numero progressivo dell'anno")
        ]
        public int NumeroProgressivoAnno { get; set; }

        [
            Display(Name = "Anno"),
            Required(ErrorMessage = "Inserisci l'anno")
        ]
        public int Year { get; set; }

        [
            Display(Name = "Data inizio permanenza"),
            Required(ErrorMessage = "Inserisci la data")
        ]
        public DateTime StartDate { get; set; }

        [
           Display(Name = "Data fine permanenza"),
           Required(ErrorMessage = "Inserisci la data")
        ]
        public DateTime EndDate { get; set; }

        [
            Display(Name = "Caparra confirmatoria"),
            Required(ErrorMessage = "Inserisci la cifra")
        ]
        public decimal Caparra { get; set; }

        [
           Display(Name = "Tariffa"),
           Required(ErrorMessage = "Inserisci tariffa")
       ]
        public decimal Tax { get; set; }

        [
           Display(Name = "Tipologia trattamento di permanenza"),
           Required(ErrorMessage = "Inserisci la tipologia di permanenza", AllowEmptyStrings = false),
            StringLength(50, MinimumLength = 14, ErrorMessage = "Inserisci correttamente la tipologia 'Prima Colazione', 'Pensione Completa', 'Mezza Pensione', minimo 14 caratteri")
       ]
        public string TypeOfStay { get; set; }
    }
}
