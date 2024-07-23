using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DbContext
    {
        public IRoleDao Roles { get; set; }

        public IUserDao Users { get; set; }

        public IUserRoleDao UsersRoles { get; set; }

        public DbContext(IRoleDao roleDao, IUserRoleDao userRoleDao, IUserDao userDao) {
            Roles  = roleDao;
            Users = userDao;
            UsersRoles = userRoleDao;
        }
    }
}
