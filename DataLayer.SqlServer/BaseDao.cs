using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SqlServer
{
    public class BaseDao
    {
        protected readonly string connectionString;
        protected readonly ILogger<UserDao> logger;

        public BaseDao(IConfiguration configuration, ILogger<UserDao> logger)
        {
            this.logger = logger;
            connectionString = configuration.GetConnectionString("MyDb")!;
        }
    }
}
