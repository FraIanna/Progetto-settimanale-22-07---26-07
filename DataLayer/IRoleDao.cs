using DataLayer.Data;

namespace DataLayer
{
    public interface IRoleDao
    {
        RoleEntity Create(string roleName);

        RoleEntity Delete(string roleName);

        RoleEntity Get(string roleName);

        List<string> GetAll();
    }
}
