using ClassRoomManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public class InMemoryClassRoomManagerData : IClassRoomManagerData
    {
        private IEnumerable<Group> Groups;
        private IEnumerable<Student> Students;
        private IList<Team> Teams;

        public InMemoryClassRoomManagerData()
        {
            Groups = new List<Group>
            {
                new Group
                {
                    GroupId = 1,
                    Name = "A",
                    StudentsList = new List<Student>
                    {
                        new Student()
                        {
                            GroupId = 1,
                            ListNumber = 1,
                            Name = "Edwin Perez",
                            StudentId = 1
                        }
                    },
                    TeamsList = default
                },
                new Group
                {
                    GroupId = 2,
                    Name = "B",
                    StudentsList = new List<Student>
                    {
                        new Student()
                        {
                            GroupId = 2,
                            ListNumber = 1,
                            Name = "Alicia Paredes",
                            StudentId = 1
                        }
                    },
                    TeamsList = default
                }
            };

            Students = new List<Student>()
            {
                new Student()
                {
                    GroupId = 1,
                    ListNumber = 1,
                    Name = "Edwin Perez",
                    StudentId = 1
                },
                new Student()
                {
                    GroupId = 2,
                    ListNumber = 1,
                    Name = "Alicia Paredes",
                    StudentId = 1
                }
            };
            Teams = new List<Team>();
        }

        public int AddTeam(Team team)
        {
            if (team != null)
            {
                try
                {
                    team.TeamId = Teams.Count;
                    Teams.Add(team);
                }
                catch (Exception)
                {
                    return -1;
                    
                }
            }
            return 1;
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return Groups;
        }

        public Group GetGroupById(int groupId)
        {
            return Groups.Where(g => g.GroupId == groupId).FirstOrDefault();
        }

        public Student GetStudentById(int studentId)
        {
            return Students.Where(s => s.StudentId == studentId).FirstOrDefault();
        }

        public IEnumerable<Student> GetStudentsByGroupId(int groupId)
        {
            return Students.Where(s => s.GroupId == groupId);
        }

        public IEnumerable<Team> GetTeamsByGroupId(int groupId)
        {
            return Teams.Where(t => t.GroupId == groupId);
        }

        Team IClassRoomManagerData.GetTeamById(int teamId)
        {
            return Teams.Where(t => t.TeamId == teamId).FirstOrDefault();
        }
    }
}
