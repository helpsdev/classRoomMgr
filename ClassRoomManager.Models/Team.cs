using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClassRoomManager.Models
{
    public class Team
    {
        [Display(Name="Nombre")]
        public string Name { get; set; }
        public string Color { get; set; }
        [NotMapped]
        [Required]
        public IEnumerable<int> StudentIds { get; set; }
        public ICollection<Student> StudentList { get; set; } = new List<Student>();
        public int TeamId { get; set; }
    }
}
