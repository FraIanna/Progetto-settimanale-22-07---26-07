using DataLayer.Data;

namespace DataLayer
{
    public interface IServiceDao
    {
        ServiceEntity Create(ServiceEntity service);

        ServiceEntity Delete(int serviceId);

        ServiceEntity Get(int serviceId);

        IEnumerable<ServiceEntity> GetAll();

        void Update(ServiceEntity service);
    }
}
