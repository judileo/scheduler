using scheduler.core.Dtos.Requests;
using scheduler.core.Dtos.Responses;
using scheduler.core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.core.Interfaces
{
    public interface IEventStatusService
    {
        List<GetEventStatusDto> GetAll();
        Result<string> Create(CreateEventStatusDto dto);
        Result Delete(int id);
    }
}
