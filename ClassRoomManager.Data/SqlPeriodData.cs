using ClassRoomManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClassRoomManager.Repositories
{
    public class SqlPeriodData : IPeriodData
    {
        public ClassRoomManagerContext ClassRoomManagerContext { get; }
        public SqlPeriodData(ClassRoomManagerContext classRoomManagerContext)
        {
            ClassRoomManagerContext = classRoomManagerContext;
        }

        public IEnumerable<Period> GetAllPeriods()
        {
            return ClassRoomManagerContext.Periods.ToList();
        }

        public int AddPeriod(Period period)
        {
            ClassRoomManagerContext.Periods
                .Add(period);
            return ClassRoomManagerContext.SaveChanges();
        }

        public int UpdatePeriod(Period period)
        {
            ClassRoomManagerContext.Periods
                .Update(period);
            return ClassRoomManagerContext.SaveChanges();
        }

        public Period GetPeriodById(int periodId)
        {
            return ClassRoomManagerContext.Periods
                .FirstOrDefault(p => p.PeriodId == periodId);
        }

        public int DeletePeriod(Period period)
        {
            ClassRoomManagerContext.Periods.Remove(period);
            return ClassRoomManagerContext.SaveChanges();
        }
    }
}
