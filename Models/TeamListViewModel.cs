using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRoomManager.Models
{
    public class TeamListViewModel
    {
        public IEnumerable<Team> Teams { get; set; }
        public int GroupId { get; set; }
        public IEnumerable<ActivityAssignment> ActivitiesAssigned { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
        public StudentClassDay StudentClassDay { get; set; }

    }
}
