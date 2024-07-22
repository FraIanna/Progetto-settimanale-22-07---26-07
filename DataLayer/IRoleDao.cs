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
        RoleEntity Create (RoleEntity role);

        RoleEntity Update (int RoleId, RoleEntity role);

        RoleEntity Delete (RoleEntity role);

        RoleEntity Get (int RoleId);

        List<UserEntity> GetAll();
    }
}
