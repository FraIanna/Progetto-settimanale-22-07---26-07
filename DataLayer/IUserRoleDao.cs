namespace DataLayer
{
    public interface IUserRoleDao
    {
        void Create(int userId, int roleId);

        void Delete(int userId, int roleId);

        List<string> GetAllByUsername(string username);
    }
}
