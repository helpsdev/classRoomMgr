using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Models
{
    public class Team
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public IEnumerable<Student> StudentsList { get; set; }
        public int TeamId { get; set; }
    }
}
