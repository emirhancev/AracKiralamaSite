using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public interface IUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
