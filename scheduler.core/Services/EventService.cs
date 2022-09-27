using scheduler.core.Dtos.Requests;
using scheduler.core.Entities;
using scheduler.core.Interfaces;

namespace scheduler.core.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }


        public object GetAll()
        {
            var result = _eventRepository.GetAll();
            return result;
        }

        public object Create(CreateEventDto req)
        {
            var entity = new Event()
            {
                Day = req.Day,
                Begin = req.Begin,
                End = req.End,
                MaxCapacity = req.MaxCapacity,
                Instructor = null,
                Description = req.Description,
            };

            _eventRepository.Create(entity);

            return "ok";
        }
    }
}
