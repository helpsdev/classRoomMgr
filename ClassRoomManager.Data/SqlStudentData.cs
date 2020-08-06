using ClassRoomManager.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        public ICollection<StudentClassDay> GetStudentClassDaysByDate(DateTimeOffset date)
        {
            return ClassRoomManagerDbContext.StudentClassDays
                .Include(scd => scd.ClassDay)
                .Where(scd => scd.ClassDay.DateTime.Date == date.Date)
                .ToList();
        }

        public int AddOrUpdateStudentClassDay(StudentClassDay studentClassDay)
        {
            if (studentClassDay.StudentClassDayId == 0 || ClassRoomManagerDbContext.StudentClassDays.Find(studentClassDay.StudentClassDayId) == null)
            {
                ClassRoomManagerDbContext.StudentClassDays.Add(studentClassDay);
                return ClassRoomManagerDbContext.SaveChanges();
            }
            else
            {
                ClassRoomManagerDbContext.StudentClassDays.Update(studentClassDay);
                return ClassRoomManagerDbContext.SaveChanges();
            }
        }
    }
}
