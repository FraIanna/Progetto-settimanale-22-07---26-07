using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
