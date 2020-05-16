using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Models
{
    public class StudentClassDay
    {
        public Student Student { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool Assistance { get; set; }
        public int Grade { get; set; }
    }
}
