using System;
using System.Collections.Generic;
using System.Text;

namespace BitstampApp.Core.Models
{
    public class User : Base
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
    }
}
