using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using scheduler.core.Interfaces;
using scheduler.core.Mappings;
using scheduler.core.Wrappers;
using System;
using System.Collections.Generic;

namespace scheduler.core.Services
{
    public sealed class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }


        public IEnumerable<GetEventDto> GetAll()
        {
            var result = _eventRepository.GetAll();

            var response = EventMapping.FromEntityToDtoList(result);

            return response;
        }


        public Result Create(CreateEventDto req)
        {
            var entity = EventMapping.FromDtoToEntity(req);

            _eventRepository.Create(entity);

            return Result.Success();
        }


        public Result Delete(Guid eventId) // TODO: Luego esto solo lo va a poder ejecutar un usuario admin solo si el curso ya está en estado 'cancelado' 
        {
            var entityToDelete = _eventRepository.GetById(eventId);

            if (entityToDelete is null)
                return Result.NotFound();

            _eventRepository.Delete(entityToDelete);

            return Result.Success();

        }


        public Result CancelEvent(CancelEventDto dto) // TODO: Esto lo puede ejecutar un usuario 'Admin' y 'Profe'
        {
            var result = _eventRepository.ChangeStatus(dto.Id);

            if (result)
            {
                return Result.Success();
            }
            else
            {
                return Result.Fail($"The product wasn´t found");
            }
        }
    }
}
