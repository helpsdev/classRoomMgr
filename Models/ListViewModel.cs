using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassRoomManager.Models
{
    public class ListViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}
