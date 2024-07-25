using DataLayer.Data;

namespace DataLayer.DaoInterfaces
{
    public interface IRoomDao
    {
        RoomEntity Create(RoomEntity room);

        RoomEntity Delete(int roomNumber);

        RoomEntity Get(int roomNumber);

        IEnumerable<RoomEntity> GetAll();

        void Update(RoomEntity room);
    }
}
