using ClassRoomManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public interface IGroupData
    {
        IEnumerable<Group> GetAllGroups();
        Group GetGroupById(int groupId);
        int AddActivity(int groupId, int activityId);
    }
}
