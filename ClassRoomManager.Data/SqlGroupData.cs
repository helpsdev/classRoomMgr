using ClassRoomManager.Models;
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
    }
}
