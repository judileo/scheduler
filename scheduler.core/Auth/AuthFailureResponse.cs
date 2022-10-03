using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler.core.Auth
{
    public class AuthFailureResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
