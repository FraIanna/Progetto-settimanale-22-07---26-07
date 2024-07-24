using Microsoft.Extensions.DependencyInjection;

namespace DataLayer.SqlServer
{
    public static class Helpers
    {
        public static IServiceCollection RegisterDAOs(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserDao, UserDao>()
                .AddScoped<IRoleDao, RoleDao>()
                .AddScoped<IUserRoleDao, UserRoleDao>()
                .AddScoped<IServiceDao, ServiceDao>()
                .AddScoped<IClientDao, ClientDao>()
                .AddScoped<IBookingDao, BookingDao>()
                .AddScoped<IBookingServiceDao, BookingServiceDao>()
                .AddScoped<IRoomDao, RoomDao>()
                ;
        }
    }
}
