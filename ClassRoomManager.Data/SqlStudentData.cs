using ClassRoomManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public class SqlStudentData : IStudentData
    {
        public ClassRoomManagerContext ClassRoomManagerDbContext { get; }
        public SqlStudentData(ClassRoomManagerContext classRoomManagerContext)
        {
            ClassRoomManagerDbContext = classRoomManagerContext;
        }
                

        public Student GetStudentById(int studentId)
        {
            return ClassRoomManagerDbContext.Students
                .Where(s => s.StudentId == studentId).FirstOrDefault();
        }

        public ICollection<Student> GetStudentsByGroupId(int groupId)
        {
            return ClassRoomManagerDbContext.Students
                .Where(s => s.Group.GroupId == groupId)
                .ToList();
        }
    }
}
