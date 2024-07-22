using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Username"), Required(AllowEmptyStrings = false, ErrorMessage = "Inserisci lo Username")]
        public string Username { get; set; }

        [
            Display(Name = "Password"), 
            Required(AllowEmptyStrings = false, ErrorMessage = "Inserisci la Password"),
            DataType(DataType.Password),
            StringLength((50), MinimumLength = (8), ErrorMessage = "La password deve contenere almeno 8 caratteri"),
        ]
        public string Password { get; set; }

        public List<string> Roles { get; set; } = [];
    }
}
