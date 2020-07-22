using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassRoomManager.Models
{
    public enum ActivityType
    {
        [Display(Name = "Calificación")]
        Grade = 0,
        [Display(Name = "Completada")]
        Task
    }
}
