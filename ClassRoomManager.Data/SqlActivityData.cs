using ClassRoomManager.Models;
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
                activity.ModificationDate = DateTimeOffset.Now;
            ClassRoomManagerContext.Activities.Add(activity);
            return ClassRoomManagerContext.SaveChanges();
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            return ClassRoomManagerContext.Activities.ToList();
        }

        public void UpdateActivity(Activity activity)
        {
            activity.ModificationDate = DateTimeOffset.Now;
            ClassRoomManagerContext.Activities.Update(activity);
        }
    }
}
