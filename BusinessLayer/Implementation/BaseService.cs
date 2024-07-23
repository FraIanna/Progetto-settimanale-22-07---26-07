using DataLayer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
