using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Models
{
     public class ClassDay
    {
        public int ClassDayId { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public int PeriodId { get; set; }
        public Period Period { get; set; }
    }
}
