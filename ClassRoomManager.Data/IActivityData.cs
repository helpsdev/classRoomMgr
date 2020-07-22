using ClassRoomManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public interface IActivityData
    {
        IEnumerable<Activity> GetAllActivities();
        int AddActivity(Activity activity);
        void UpdateActivity(Activity activity);

    }
}
