using BusinessLayer.Data;

namespace BusinessLayer
{
    public interface IAccountService
    {
        UserDto Register(UserDto user);

        UserDto? Login(string username, string password);

        List<UserDto> GetAllUsers();

        List<string> GetAllRoles();

        List<string> GetUserRoles(string username);

        UserDto GetByUsername(string username);

        bool AddUserToRole(string username, string roleName);

        bool RemoveUserFromRole(string username, string roleName);

        bool IsUserInRole(string username, string roleName);
    }
}
