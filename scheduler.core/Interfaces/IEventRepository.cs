using scheduler.core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scheduler.core.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(Guid eventId);
        Task CreateAsync(Event entity);
        void Delete(Event entity);
        Task<bool> ChangeStatusAsync(Guid dto);
    }
}
