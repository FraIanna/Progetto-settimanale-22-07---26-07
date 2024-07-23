using DataLayer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer
{
    public class RoleDao : BaseDao, IRoleDao
    {
        public RoleDao(IConfiguration configuration, ILogger<UserDao> logger) : base(configuration, logger) { }

        public RoleEntity Create(string roleName)
        {
            throw new NotImplementedException();
        }


        public RoleEntity Delete(string roleName)
        {
            throw new NotImplementedException();
        }

        public RoleEntity Get(string roleName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
