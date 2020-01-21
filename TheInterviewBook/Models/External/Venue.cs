using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheInterviewBook.Models.External
{
    public class Venue
    {
        public string name { get; set; }
        public string link { get; set; }
        public string city { get; set; }
        public TimeZone timeZone { get; set; }
        public int? id { get; set; }
    }
}
