using scheduler.core.Entities;
using System.Collections.Generic;


namespace scheduler.core.Interfaces
{
    public interface IEventStatusRepository
    {
        List<EventStatus> GetAll();
        //EventStatus GetById(int id);
        void Create(EventStatus entity);
        bool Delete(int id);
    }
}
