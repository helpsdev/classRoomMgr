using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRoomManager.Models
{
    public class StudentDetailsViewModel
    {
        public Student Student { get; set; }
        public int TotalClasses { get; set; }
        public int ClassesTaken { get; set; }
    }
}
