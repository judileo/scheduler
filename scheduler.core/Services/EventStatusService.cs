using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using scheduler.core.Interfaces;
using scheduler.core.Mappings;
using scheduler.core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.core.Services
{
    public class EventStatusService : IEventStatusService
    {
        private readonly IEventStatusRepository _eventRepository;

        public EventStatusService(IEventStatusRepository repository)
        {
            _eventRepository = repository;
        }

        public List<GetEventStatusDto> GetAll()
        {
            var result = _eventRepository.GetAll();

            var response = EventStatusMapping.FromEntityToDtoList(result);

            return response;
        }

        public Result<string> Create(CreateEventStatusDto req)
        {
            var entity = EventStatusMapping.FromDtoToEntity(req);

            _eventRepository.Create(entity);

            return Result<string>.Success($"{entity.EventStatusName}");
        }

        public Result Delete(int id)
        {
            var result = _eventRepository.Delete(id);

            return result
                ? Result.Success()
                : Result.Fail(default);
        }
    }
}
