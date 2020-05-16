using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Data.Models
{
    public class Group
    {
        public string Name { get; set; }
        public IEnumerable<Student> StudentsList { get; set; }
        public IEnumerable<Team> TeamsList { get; set; }
        public int GroupId { get; set; }
    }
}
