using ClassRoomManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public interface ITeamData
    {
        int AddTeam(Team team);
        Team GetTeamById(int teamId);
        IEnumerable<Team> GetTeamsByGroupId(int groupId);
    }
}
