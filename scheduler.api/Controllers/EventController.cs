using Microsoft.AspNetCore.Mvc;
using scheduler.core.Dtos.Requests;
using scheduler.core.Interfaces;

namespace scheduler.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _eventService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateEventDto req)
        {
            var result = _eventService.Create(req);
            return Ok(result);
        }
    }
}
