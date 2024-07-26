using System.ComponentModel.DataAnnotations;

namespace DataLayer.Data
{
    public class RoomEntity
    {
        public int Id { get; set; }

        [Display(Name = "Numero Camera"), Required]
        public int RoomNumber { get; set; }

        [
            Display(Name = "Descrizione"),
            Required(ErrorMessage = "Inserisci la descrizione", AllowEmptyStrings = false),
            StringLength(300)
        ]
        public string Description { get; set; }

        [Display(Name = "Tipologia"), Required]
        public bool Type { get; set; }

    }
}
