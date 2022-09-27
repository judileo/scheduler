using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using scheduler.core.Wrappers;
using System;
using System.Collections.Generic;

namespace scheduler.core.Interfaces
{
    public interface IEventService
    {
        IEnumerable<GetEventDto> GetAll();
        Result Create(CreateEventDto req);
        Result Delete(Guid eventId);
    }
}
