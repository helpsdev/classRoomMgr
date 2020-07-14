using ClassRoomManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public class SqlTeamData : ITeamData
    {
        public ClassRoomManagerContext ClassRoomManagerDbContext { get; }
        public SqlTeamData(ClassRoomManagerContext classRoomManagerContext)
        {
            ClassRoomManagerDbContext = classRoomManagerContext;
        }

        public int AddTeam(Team team)
        {
            ClassRoomManagerDbContext.Teams.Add(team);
            return ClassRoomManagerDbContext.SaveChanges();
        }

        public Team GetTeamById(int teamId)
        {
            return ClassRoomManagerDbContext.Teams
                .Where(t => t.TeamId == teamId)
                .FirstOrDefault();
        }

        public IEnumerable<Team> GetTeamsByGroupId(int groupId)
        {
            return ClassRoomManagerDbContext.Teams
                .Include(t => t.StudentList)
                    .ThenInclude(s => s.Group)
                .Where(t => t.StudentList.All(s => s.GroupId == groupId))
                .ToList();
        }
    }
}
