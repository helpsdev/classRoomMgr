using ClassRoomManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public interface IPeriodData
    {
        IEnumerable<Period> GetAllPeriods();
        int AddPeriod(Period period);
        int UpdatePeriod(Period period);
        Period GetPeriodById(int periodId);
        int DeletePeriod(Period period);
        Period GetPeriodForDate(DateTimeOffset targetDateTime);
    }
}
