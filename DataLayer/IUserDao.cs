using DataLayer.Data;

namespace DataLayer
{
    public interface IUserDao
    {
        UserEntity Create(UserEntity user);

        UserEntity Update(int UserId, UserEntity user);

        UserEntity Delete(int id);

        UserEntity Get(int id);

        UserEntity GetByUsername(string username);

        public List<UserEntity> GetAll();

        UserEntity? Login(string username);
    }
}
