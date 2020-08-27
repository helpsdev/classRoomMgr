using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClassRoomManager.Models
{
    public class StudentFinalGrade
    {
        public int StudentFinalGradeId { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset ModificationDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalGrade { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int PeriodId { get; set; }
        public Period Period { get; set; }
    }
}
