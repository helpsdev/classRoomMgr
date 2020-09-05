using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassRoomManager.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalEvaluationValue { get; set; }
        public ActivityType Type { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset ModificationDate { get; set; }
        public int? PeriodId { get; set; }
        public Period Period { get; set; }
    }
}