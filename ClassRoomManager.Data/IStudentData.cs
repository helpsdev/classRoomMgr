using ClassRoomManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public interface IStudentData
    {
        IEnumerable<Student> GetStudentsByGroupId(int groupId);
        Student GetStudentById(int studentId);
    }
}
