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
            ClassRoomManagerContext.Activities.Add(activity);
            return ClassRoomManagerContext.SaveChanges();
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            return ClassRoomManagerContext.Activities.ToList();
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
    }
}
