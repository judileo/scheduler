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


        [HttpDelete("{eventId}")]
        public IActionResult Delete([FromRoute] Guid eventId)
        {
            var result = _eventService.Delete(eventId);

            _logger.Information(JsonConvert.SerializeObject(result));

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);

        }

        [HttpPatch]
        public ActionResult ChangeEventStatus([FromBody] ChangeEventStatusDto dto)
        {
            var result = _eventService.ChangeStatus(dto);

            return Ok(result);
        }
    }
}
