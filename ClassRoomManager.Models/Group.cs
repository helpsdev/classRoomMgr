using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Models
{
    public class Group
    {
        public string Name { get; set; }
        public int GroupId { get; set; }
        public ICollection<Student> StudentsList { get; set; }
        
    }
}
