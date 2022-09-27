using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using scheduler.core.Entities;
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
                maxCapacity: dto.MaxCapacity,
                instructor: null,
                description: dto.Description);

            return entity;
        }

        public static IEnumerable<GetEventDto> FromEntityToDtoList(List<Event> entities)
        {
            //var response = new List<GetEventDto>();

            var response = entities.Select(x => new GetEventDto()
            {
                Id = x.Id,
                Day = x.Day,
                Begin = x.Begin,
                End = x.End,
                MaxCapacity = x.MaxCapacity,
                Instructor = x.Instructor,
                Students = x.Students,
                Description = x.Description,
            });

            return response;
        }
    }
}
