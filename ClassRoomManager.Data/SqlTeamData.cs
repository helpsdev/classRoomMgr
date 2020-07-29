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
            /*Look up students in team's studentIds list and add them into the team's studentList*/
            ClassRoomManagerDbContext.Students
                .Where(s => team.StudentIds.Contains(s.StudentId))
                .ForEachAsync(s => team.StudentList.Add(s));
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
            return ClassRoomManagerDbContext.StudentClassDays
                .Include(scd => scd.Student)
                    .ThenInclude(s => s.Team)
                .Include(scd => scd.ClassDay)
                .Where(scd => scd.ClassDay.DateTime.Date == DateTimeOffset.Now.Date && scd.Student.GroupId == groupId)
                .Select(s => s.Student.Team)
                .ToList();
        }
    }
}
