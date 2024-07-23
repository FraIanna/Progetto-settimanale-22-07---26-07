using System.ComponentModel.DataAnnotations;

namespace Progetto_settimanale_22_07___26_07.Models
{
    public class AccountModel
    {
        [Display(Name = "Username"), Required(AllowEmptyStrings = false, ErrorMessage = "Inserisci lo Username")]
        public required string Username { get; set; }

        [
           Display(Name = "Password"),
           Required(AllowEmptyStrings = false, ErrorMessage = "Inserisci la Password"),
           DataType(DataType.Password),
           StringLength((50), MinimumLength = (8), ErrorMessage = "La password deve contenere almeno 8 caratteri"),
       ]
        public required string Password { get; set; }
    }
}
