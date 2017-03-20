using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetCoreDemos.ProjectReferencing.NetCore.Data
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
    }
}
