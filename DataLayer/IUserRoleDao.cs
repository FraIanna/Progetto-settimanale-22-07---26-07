using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IUserRoleDao
    {
        UserRoleEntity Create (UserRoleEntity userRole); 

        UserRoleEntity Delete (int UserId, int RoleId);
    }
}
