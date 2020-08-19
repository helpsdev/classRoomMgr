using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassRoomManager.Models
{
    public class Period : IValidatableObject
    {
        public int PeriodId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate.Date < StartDate.Date)
            {
                yield return new ValidationResult($"La fecha de termino {EndDate} debe ser mayor a la fecha de inicio {StartDate}.", new[] { nameof(EndDate) });
            }
        }
    }
}
