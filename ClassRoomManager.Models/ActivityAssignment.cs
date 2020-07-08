using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Models
{
    public class ActivityAssignment
    {
        public int ActivityAssignmentId { get; set; }
        public bool Completed { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
