using DataLayer;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Implementation
{
    public class BaseService
    {
        protected readonly DbContext dbContext;
        protected readonly ILogger<BaseService> logger;

        public BaseService(DbContext dbContext, ILogger<BaseService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
    }
}
