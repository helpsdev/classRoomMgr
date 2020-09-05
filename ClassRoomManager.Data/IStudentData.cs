using ClassRoomManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public interface IStudentData
    {
        ICollection<Student> GetStudentsByGroupId(int groupId);
        Student GetStudentById(int studentId);
        ICollection<StudentClassDay> GetStudentClassDaysByDate(DateTimeOffset date);
        int AddStudentClassDay(StudentClassDay studentClassDay);
        int UpdateStudentClassDay(StudentClassDay studentClassDay);
        int AddOrUpdateStudentClassDay(StudentClassDay studentClassDay);
        ICollection<StudentClassDay> GetStudentClassDaysByStudentId(int studentId);
        ClassDay GetTodayClassDay();
        ICollection<StudentFinalGrade> GetAllStudentFinalGrades();
    }
}
