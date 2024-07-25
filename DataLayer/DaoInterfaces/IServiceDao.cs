using DataLayer.Data;

namespace DataLayer.DaoInterfaces
{
    public interface IServiceDao
    {
        ServiceDto Create(ServiceDto service);

        ServiceEntity Delete(int serviceId);

        ServiceEntity Get(int serviceId);

        IEnumerable<ServiceEntity> GetAll();

        void Update(ServiceEntity service);
    }
}
