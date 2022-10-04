using scheduler.core.Entities;
using System;
using System.Collections.Generic;

namespace scheduler.core.Interfaces
{
    public interface IEventRepository
    {
        List<Event> GetAll();
        Event GetById(Guid eventId);
        void Create(Event entity);
        void Delete(Event entity);
        bool ChangeStatus(Guid dto);
    }
}
