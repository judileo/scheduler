using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using scheduler.core.Dtos.Requests;
using scheduler.core.Interfaces;
using Serilog;
using System;

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
        public IActionResult GetAll()
        {
            var result = _eventService.GetAll();

            _logger.Information(JsonConvert.SerializeObject(result));

            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateEventDto req)
        {
            var result = _eventService.Create(req);

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
        public IActionResult Delete([FromRoute] Guid eventId)
        {
            var result = _eventService.Delete(eventId);

            _logger.Information(JsonConvert.SerializeObject(result));

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);

        }

        [HttpPut("cancel-event")]
        public ActionResult CancelEventStatus([FromBody] CancelEventDto dto)
        {
            var result = _eventService.CancelEvent(dto);

            return Ok(result);
        }
    }
}
