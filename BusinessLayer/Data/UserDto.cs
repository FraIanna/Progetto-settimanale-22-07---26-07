using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Data
{
    public class UserDto
    {
        public int Id { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        public List<string> Roles { get; set; } = [];
    }
}
