using scheduler.core.Entities;
using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using System.Collections.Generic;
using System.Linq;

namespace scheduler.core.Mappings
{
    public static class EventStatusMapping
    {
        public static EventStatus FromDtoToEntity(CreateEventStatusDto dto)
        {
            var entity = new EventStatus(
                EventStatusName : dto.Name);

            return entity;
        }

        public static List<GetEventStatusDto> FromEntityToDtoList(List<EventStatus> entities)
        {
            var response = entities.Select(x => new GetEventStatusDto()
            {
                Id = x.EventStatusId,
                Name = x.EventStatusName,
            });

            return (List<GetEventStatusDto>)response;
        }
    }
}
