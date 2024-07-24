namespace DataLayer
{
    public class DbContext
    {
        public IRoleDao Roles { get; set; }

        public IUserDao Users { get; set; }

        public IUserRoleDao UsersRoles { get; set; }

        public IClientDao Clients { get; set; }

        public IRoomDao Rooms { get; set; }

        public IBookingDao Bookings { get; set; }

        public IServiceDao Services { get; set; }

        public IBookingServiceDao BookingsService { get; set; }

        public DbContext
            (IRoleDao roleDao,
            IUserRoleDao userRoleDao,
            IUserDao userDao,
            IClientDao clientDao,
            IRoomDao roomDao,
            IBookingDao bookingDao,
            IServiceDao serviceDao,
            IBookingServiceDao bookingsServiceDao
            )
        {
            Roles = roleDao;
            Users = userDao;
            UsersRoles = userRoleDao;
            Clients = clientDao;
            Rooms = roomDao;
            Bookings = bookingDao;
            Services = serviceDao;
            BookingsService = bookingsServiceDao;
        }
    }
}
