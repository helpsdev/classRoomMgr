using ClassRoomManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public class SqlGroupData : IGroupData
    {
        public ClassRoomManagerContext ClassRoomManagerDbContext { get; }
        public SqlGroupData(ClassRoomManagerContext classRoomManagerDbContext)
        {
            ClassRoomManagerDbContext = classRoomManagerDbContext;
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return ClassRoomManagerDbContext.Groups
                .ToList();
        }

        public Group GetGroupById(int groupId)
        {
            return ClassRoomManagerDbContext.Groups
                .Where(g => g.GroupId == groupId)
                .FirstOrDefault();
        }

        public int AddActivity(int groupId, int activityId)
        {
            var group = ClassRoomManagerDbContext.Groups
                .Include(g => g.StudentsList)
                .Where(g => g.GroupId == groupId).FirstOrDefault();

            if (group != null)
            {
                foreach (var student in group.StudentsList)
                {
                    ClassRoomManagerDbContext.ActivityAssignments
                        .Add(new ActivityAssignment
                        {
                            ActivityId = activityId,
                            StudentId = student.StudentId
                        });
                }
            }

            return ClassRoomManagerDbContext.SaveChanges();
                
        }
    }
}
