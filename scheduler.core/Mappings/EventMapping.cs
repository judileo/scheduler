using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using scheduler.core.Entities;
using scheduler.core.Enums;
using System.Collections.Generic;
using System.Linq;

namespace scheduler.core.Mappings
{
    public static class EventMapping
    {
        public static Event FromDtoToEntity(CreateEventDto dto)
        {
            var entity = new Event(
                day: dto.Day,
                begin: dto.Begin,
                end: dto.End,
                eventStatusId: dto.EventStatusId != default
                                ? dto.EventStatusId
                                : (int)EventStatusEnum.Available,
                maxCapacity: dto.MaxCapacity,
                instructor: null, // TODO: Luego mapear este dato, actualmente no lo asocia nunca
                description: dto.Description);

            return entity;
        }

        public static IEnumerable<GetEventDto> FromEntityToDtoList(List<Event> entities)
        {
            var response = entities.Select(x => new GetEventDto()
            {
                Id = x.Id,
                Day = x.Day,
                Begin = x.Begin,
                End = x.End,
                MaxCapacity = x.MaxCapacity,
                EventStatusId = x.EventStatusId,
                Instructor = x.Instructor,
                Students = x.Students,
                Description = x.Description,
            });

            return response;
        }
    }
}
