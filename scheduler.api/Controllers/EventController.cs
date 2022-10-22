using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using scheduler.core.Dtos.Requests;
using scheduler.core.Interfaces;
using Serilog;
using System;
using System.Threading.Tasks;

namespace scheduler.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ILogger _logger;

        public EventController(IEventService eventService, ILogger logger)
        {
            _eventService = eventService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _eventService.GetAllAsync();

            _logger.Information(JsonConvert.SerializeObject(result));

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEventDto req)
        {
            var result = await _eventService.CreateAsync(req);

            _logger.Information(JsonConvert.SerializeObject(result));

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }


        [HttpPost("add-users-to-event")]
        public IActionResult AddUsersToEvent([FromBody] AddUsersToEventDto req)
        {
            // TODO: Asociar la lista de usuarios recibidos al evento especificado
            // Validar que los usuarios y el evento existan

            return default;
        }


        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid eventId)
        {
            var result = await _eventService.DeleteAsync(eventId);

            _logger.Information(JsonConvert.SerializeObject(result));

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);

        }

        [HttpPut("cancel-event")]
        public async Task<ActionResult> CancelEventStatusAsync([FromBody] CancelEventDto dto)
        {
            var result = await _eventService.CancelEventAsync(dto);

            return Ok(result);
        }
    }
}
