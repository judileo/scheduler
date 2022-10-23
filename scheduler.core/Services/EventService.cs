using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using scheduler.core.Interfaces;
using scheduler.core.Mappings;
using scheduler.core.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scheduler.core.Services
{
    public sealed class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }


        public async Task<IEnumerable<GetEventDto>> GetAllAsync()
        {
            var result = await _eventRepository.GetAllAsync();

            var response = EventMapping.FromEntityToDtoList(result);

            return response;
        }


        public async Task<Result> CreateAsync(CreateEventDto req)
        {
            var entity = EventMapping.FromDtoToEntity(req);

            await _eventRepository.CreateAsync(entity);

            return Result.Success();
        }


        public async Task<Result> DeleteAsync(Guid eventId) // TODO: Luego esto solo lo va a poder ejecutar un usuario admin solo si el curso ya está en estado 'cancelado' 
        {
            var entityToDelete =  await _eventRepository.GetByIdAsync(eventId);

            if (entityToDelete is null)
                return Result.NotFound();

            _eventRepository.Delete(entityToDelete);

            return Result.Success();

        }


        public async Task<Result> CancelEventAsync(CancelEventDto dto) // TODO: Esto lo puede ejecutar un usuario 'Admin' y 'Profe'
        {
            var result =  await _eventRepository.ChangeStatusAsync(dto.Id);

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
