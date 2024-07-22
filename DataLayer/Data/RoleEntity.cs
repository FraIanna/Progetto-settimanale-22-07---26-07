using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class RoleEntity
    {
        public int Id { get; set; }

        [Display(Name = "Ruolo")]
        public string Name { get; set; }
    }
}
