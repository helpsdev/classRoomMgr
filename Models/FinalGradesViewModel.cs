using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRoomManager.Models
{
    public class FinalGradesViewModel
    {
        public StudentFinalGrade[] StudentFinalGrades { get; set; }
        public int PeriodId { get; set; }
    }
}
