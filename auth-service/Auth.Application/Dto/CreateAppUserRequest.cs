using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Dto
{
    public class CreateAppUserRequest
    {
        public AppUserDto User { get; set; }
        public string password { get; set; }
    }
}
