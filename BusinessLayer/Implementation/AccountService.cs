using BusinessLayer.Data;
using DataLayer.Data;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Implementation
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService
            (
            DataLayer.DbContext dbContext,
            ILogger<BaseService> logger,
            IPasswordEncoder passwordEncoder
            ) : base(dbContext, logger)
        {
            _passwordEncoder = passwordEncoder;
        }

        private readonly IPasswordEncoder _passwordEncoder;

        public bool AddUserToRole(string username, string roleName)
        {
            try
            {
                var user = dbContext.Users.GetByUsername(username);
                var role = dbContext.Roles.Get(roleName);
                dbContext.UsersRoles.Create(user.Id, role.Id);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception adding user {} to role {}", username, roleName);
                return false;
            }
        }

        public List<string> GetAllRoles()
        {
            try
            {
                return dbContext.Roles.GetAll();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception retrieving all roles");
                return [];
            }
        }

        public List<UserDto> GetAllUsers()
        {
            try
            {
                var users = dbContext.Users.GetAll();
                return users.Select(u => new UserDto
                {
                    Password = u.Password,
                    Username = u.Username,
                    Id = u.Id,
                    Roles = dbContext.UsersRoles.GetAllByUsername(u.Username)
                }).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception retrieving all users");
                return [];
            }
        }

        public UserDto GetByUsername(string username)
        {
            try
            {
                var u = dbContext.Users.GetByUsername(username);
                return new UserDto
                {
                    Password = u.Password,
                    Username = u.Username,
                    Id = u.Id,
                    Roles = dbContext.UsersRoles.GetAllByUsername(username)
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception retrieving all users");
                throw;
            }
        }

        public List<string> GetUserRoles(string username)
        {
            try
            {
                return dbContext.UsersRoles.GetAllByUsername(username);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception retrieving roles for user {}", username);
                throw;
            }
        }

        public bool IsUserInRole(string username, string roleName)
        {
            try
            {
                return GetUserRoles(username).Contains(roleName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception checking if user {} is in role {}", username, roleName);
                throw;
            }
        }

        public UserDto? Login(string username, string password)
        {
            var u = dbContext.Users.Login(username);
            if (u != null && _passwordEncoder.IsSame(password, u.Password))
                return new UserDto
                {
                    Id = u.Id,
                    Password = u.Password,
                    Username = u.Username,
                    Roles = dbContext.UsersRoles.GetAllByUsername(u.Username)
                };
            return null;
        }

        public UserDto Register(UserDto user)
        {
            var u = dbContext.Users.Create(
                           new UserEntity
                           {
                               Password = _passwordEncoder.Encode(user.Password),
                               Username = user.Username
                           });
            return new UserDto { Id = u.Id, Password = u.Password, Username = u.Username };
        }

        public bool RemoveUserFromRole(string username, string roleName)
        {
            try
            {
                var user = dbContext.Users.GetByUsername(username);
                var role = dbContext.Roles.Get(roleName);
                dbContext.UsersRoles.Delete(user.Id, role.Id);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception checking if user {} is in role {}", username, roleName);
                return false;
            }
        }
    }
}
