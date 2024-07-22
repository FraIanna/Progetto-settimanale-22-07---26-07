using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class UserEntity
    {
        public int Id { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}
