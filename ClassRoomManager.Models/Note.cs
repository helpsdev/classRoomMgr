using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Models
{
    public class Note
    {
        public Group CreatedForGroup { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public Student AssignedTo { get; set; }
    }
}
