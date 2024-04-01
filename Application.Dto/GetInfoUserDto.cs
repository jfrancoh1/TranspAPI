using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class GetInfoUserDto
    {
        public UserDto User {  get; set; }

        public string token { get; set; }
    }
}
