using scheduler.core.Entities;

namespace scheduler.core.Interfaces
{
    public interface IEventRepository
    {
        object GetAll();
        void Create(Event entity);
    }
}
