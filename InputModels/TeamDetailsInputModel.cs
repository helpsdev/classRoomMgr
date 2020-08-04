using ClassRoomManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRoomManager.InputModels
{
    public class TeamDetailsInputModel
    {
        public int StudentId { get; set; }
        public StudentClassDay StudentClassDay { get; set; }
        public IEnumerable<ActivityAssignment> ActivityAssignments { get; set; }
    }
}
