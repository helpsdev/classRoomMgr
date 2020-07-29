using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassRoomManager.Models
{
    public class Student
    {
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        public int StudentId { get; set; }
        [Display(Name = "No. Lista")]
        public int ListNumber { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }

    }
}
