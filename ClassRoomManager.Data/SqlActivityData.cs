using ClassRoomManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public class SqlActivityData : IActivityData
    {
        public ClassRoomManagerContext ClassRoomManagerContext { get; }
        public SqlActivityData(ClassRoomManagerContext classRoomManagerContext)
        {
            ClassRoomManagerContext = classRoomManagerContext;
        }

        public int AddActivity(Activity activity)
        {
            activity.CreationDate =
                activity.ModificationDate = GetDateTimeOffset();

            //TODO: Replace this with IPeriodData.GetPriodForDate
            var period = GetPriodForDate(activity.CreationDate);
            activity.Period = period ?? throw new InvalidOperationException($"There is no Period for the date: {activity.CreationDate.Date}");

            ClassRoomManagerContext.Activities.Add(activity);
            return ClassRoomManagerContext.SaveChanges();
        }
        //TODO: Remove this to use IPeriodData.GetPriodForDate instead
        public Period GetPriodForDate(DateTimeOffset targetDate)
        {
            return ClassRoomManagerContext.Periods
                .FirstOrDefault(p => p.StartDate.Date <= targetDate.Date && p.EndDate.Date >= targetDate.Date);
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            return ClassRoomManagerContext.Activities
                .Include(a => a.Period)
                .ToList();
        }

        public int UpdateActivity(Activity activity)
        {
            activity.ModificationDate = GetDateTimeOffset();
            ClassRoomManagerContext.Activities.Update(activity);
            return ClassRoomManagerContext.SaveChanges();
        }

        public IEnumerable<ActivityAssignment> GetActivitiesAssignedByGroupId(int groupId)
        {
            return ClassRoomManagerContext.ActivityAssignments
                .Include(a => a.Student)
                .Where(a => a.Student.GroupId == groupId)
                .ToList();
        }

        public DateTimeOffset GetDateTimeOffset() => DateTimeOffset.Now;

        public int UpdateActivityAssignments(IEnumerable<ActivityAssignment> activityAssignments)
        {
            ClassRoomManagerContext.ActivityAssignments.UpdateRange(activityAssignments);
            return ClassRoomManagerContext.SaveChanges();
        }

        public Activity GetActivityById(int activityId)
        {
            return ClassRoomManagerContext.Activities
                .First(a => a.ActivityId == activityId);
        }
    }
}
