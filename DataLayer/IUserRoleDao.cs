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
        void Create (int userId, int roleId); 

        void Delete (int userId, int roleId);

        List<string> GetAllByUsername(string username);
    }
}
