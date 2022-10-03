using Microsoft.AspNetCore.Mvc;
using scheduler.core.Dtos.Requests;
using scheduler.core.Interfaces;

namespace scheduler.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventStatusController : ControllerBase
    {
        private readonly IEventStatusService _service;

        public EventStatusController(IEventStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var result = _service.GetAll();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateEventStatusDto dto)
        {
            var result = _service.Create(dto);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var result = _service.Delete(id);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }
    }
}
