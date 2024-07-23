using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IRoleDao
    {
        RoleEntity Create (string roleName);

        RoleEntity Delete (string roleName);

        RoleEntity Get (string roleName);

        List<string> GetAll();
    }
}
