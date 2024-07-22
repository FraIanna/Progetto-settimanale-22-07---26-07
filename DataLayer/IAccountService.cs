using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IAccountService
    {
        User Register (User user);

        User? Login (string username, string password);

        List<User> GetAllUsers ();

        List <string> GetAllRoles ();

        User GetByUsername (string username);

        bool AddUserToRole(string username, string roleName);

        bool RemoveFromRole (string username, string roleName);

        List<string> GetUserRoles(string username);
    }
}
