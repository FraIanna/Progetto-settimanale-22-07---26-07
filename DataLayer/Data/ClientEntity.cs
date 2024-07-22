using System.ComponentModel.DataAnnotations;

namespace DataLayer.Data
{
    public class ClientEntity
    {
        public int Id { get; set; }

        [
            Display(Name = "Nome"), 
            Required(ErrorMessage = "Inserisci il Nome", AllowEmptyStrings = false),
            StringLength(50)
        ]
        public string Name { get; set; }

        [
            Display(Name = "Cognome"), 
            Required(ErrorMessage = "Inserisci il Cognome", AllowEmptyStrings = false),
            StringLength(50)
        ]
        public string Surname { get; set; }

        [
            Display(Name = "Email"), 
            Required(ErrorMessage = "Inserisci la mail", AllowEmptyStrings = false),
            StringLength(50)
        ]
        public string Email { get; set; }

        [
            Display(Name = "Città"), 
            Required(ErrorMessage = "Inserisci la Città", AllowEmptyStrings = false),
            StringLength(100)
        ]
        public string City { get; set; }

        [
            Display(Name = "Provincia"), 
            Required(ErrorMessage = "Inserisci la Provincia", AllowEmptyStrings = false),
            StringLength(100)
        ]
        public string Province { get; set; }

        [
            Display(Name = "Codice Fiscale"), 
            Required(ErrorMessage = "Inserisci il Codice Fiscale", AllowEmptyStrings = false),
            StringLength(16)
        ]
        public string FiscalCode { get; set; }

        [
            Display(Name = "Cellulare"), 
            Required(ErrorMessage = "Inserisci il numero del cellulare",
            AllowEmptyStrings = false), 
            StringLength(10)
        ]
        public string CellNumber { get; set; }

        [Display(Name = "Telefono (Numero Fisso)"), StringLength(8)]
        public string PhoneNumber { get; set; }

    }
}
