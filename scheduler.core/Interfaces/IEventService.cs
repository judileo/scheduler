using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using scheduler.core.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scheduler.core.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<GetEventDto>> GetAllAsync();
        Task<Result> CreateAsync(CreateEventDto req);
        Task<Result> DeleteAsync(Guid eventId);
        Task<Result> CancelEventAsync(CancelEventDto dto);
    }
}
