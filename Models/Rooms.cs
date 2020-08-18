using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampChetek.Models
{
    public class Rooms
    {
        public int RoomsId { get; set; }
        public string Name { get; set; }

        public string StatusId { get; set; }
        public Status Status { get; set; }
    }
}
