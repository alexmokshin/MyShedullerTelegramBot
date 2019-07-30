using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramShedullerApp.DB
{
    public class MongoSettings
    {
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }

    }
}
