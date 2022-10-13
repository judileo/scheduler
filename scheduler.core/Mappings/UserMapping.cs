using scheduler.core.Dtos.Responses;
using scheduler.core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace scheduler.core.Mappings
{
    public sealed class UserMapping
    {
        //public static Event FromDtoToEntity(CreateEventDto dto)
        //{
        //    var entity = new User(
        //        day: dto.Day,
        //        begin: dto.Begin,
        //        end: dto.End,
        //        maxCapacity: dto.MaxCapacity,
        //        instructor: null,
        //        description: dto.Description);

        //    return entity;
        //}

        public static IEnumerable<GetUserResponseDto> FromEntityToDtoList(List<User> entities)
        {
            var response = entities.Select(x => new GetUserResponseDto()
            {
                Id = x.Id,
                Apellido = x.LastName,
                Nombre = x.FirstName,
            });

            return response;
        }
    }
}
