using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassRoomManager.Models
{
    public class Team
    {
        [Display(Name="Nombre")]
        public string Name { get; set; }
        public string Color { get; set; }
        public IEnumerable<Student> StudentsList { get; set; }
        public int TeamId { get; set; }
        public int GroupId { get; set; }
    }
}
