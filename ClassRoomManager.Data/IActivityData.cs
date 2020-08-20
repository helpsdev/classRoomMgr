using ClassRoomManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public interface IActivityData
    {
        IEnumerable<Activity> GetAllActivities();
        int AddActivity(Activity activity);
        int UpdateActivity(Activity activity);
        IEnumerable<ActivityAssignment> GetActivitiesAssignedByGroupId(int groupId);
        int UpdateActivityAssignments(IEnumerable<ActivityAssignment> activityAssignments);
        DateTimeOffset GetDateTimeOffset();
        Activity GetActivityById(int activityId);
    }
}
