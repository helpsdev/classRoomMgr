using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Models
{
    public class StudentClassDay
    {
        public int StudentClassDayId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public ClassDay ClassDay { get; set; }
        public int ClassDayId { get; set; }
        public bool Assistance { get; set; }
        public int Grade { get; set; }
    }
}
