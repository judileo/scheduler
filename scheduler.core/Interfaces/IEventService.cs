using scheduler.core.Dtos.Requests;

namespace scheduler.core.Interfaces
{
    public interface IEventService
    {
        object GetAll();
        object Create(CreateEventDto req);
    }
}
