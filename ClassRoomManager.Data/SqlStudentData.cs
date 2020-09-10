﻿using ClassRoomManager.Models;
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
            bool studentClassDayDoesNotExists = studentClassDay.StudentClassDayId == 0 ||
                ClassRoomManagerDbContext.StudentClassDays.Find(studentClassDay.StudentClassDayId) == null;

            if (studentClassDayDoesNotExists)
            {
                return AddStudentClassDay(studentClassDay);
            }
            else
            {
                return UpdateStudentClassDay(studentClassDay);
            }
        }

        public int AddStudentClassDay(StudentClassDay studentClassDay)
        {
            if (studentClassDay == null) throw new ArgumentException("studentClassDay");
            
            if (studentClassDay.ClassDay == null)
            {
                studentClassDay.ClassDay = GetTodayClassDay();
            }

            ClassRoomManagerDbContext.StudentClassDays.Add(studentClassDay);

            return ClassRoomManagerDbContext.SaveChanges();
        }

        public int UpdateStudentClassDay(StudentClassDay studentClassDay)
        {
            if (studentClassDay == null) throw new ArgumentException("studentClassDay");

            var dbStudentClassDay = ClassRoomManagerDbContext.StudentClassDays
                .Include(scd => scd.ClassDay)
                .FirstOrDefault(scd => scd.StudentClassDayId == studentClassDay.StudentClassDayId);

            if (dbStudentClassDay.ClassDay == null)
            {
                dbStudentClassDay.ClassDay = GetTodayClassDay();
            }

            dbStudentClassDay.Assistance = studentClassDay.Assistance;

            ClassRoomManagerDbContext.StudentClassDays.Update(dbStudentClassDay);

            return ClassRoomManagerDbContext.SaveChanges();
        }

        public ClassDay GetTodayClassDay()
        {
            var todayClassDay = ClassRoomManagerDbContext.ClassDays.FirstOrDefault(cd => cd.DateTime.Date == DateTimeOffset.Now.Date);
            if (todayClassDay == null)
            {
                todayClassDay = new ClassDay
                {
                    DateTime = DateTimeOffset.Now.Date
                };
                todayClassDay.Period = GetPriodForClassDay(todayClassDay);

                if (todayClassDay.Period == null) throw new InvalidOperationException($"There is no Period for the current ClassDay date:{todayClassDay.DateTime}");

                /*Not calling context.SaveChanges since this is being used
                 as part of another operation*/
                ClassRoomManagerDbContext.ClassDays.Add(todayClassDay);
            }
            return todayClassDay;
        }

        public Period GetPriodForClassDay(ClassDay classDay)
        {
            if (classDay == null) throw new ArgumentException("classDay");

            return ClassRoomManagerDbContext.Periods
                .FirstOrDefault(p => p.StartDate.Date <= classDay.DateTime.Date && p.EndDate.Date >= classDay.DateTime.Date);
        }

        public ICollection<StudentClassDay> GetStudentClassDaysByStudentId(int studentId)
        {
            return ClassRoomManagerDbContext.StudentClassDays
                .Include(scd => scd.Student)
                .Where(scd => scd.StudentId == studentId)
                .ToList();
        }

        public ICollection<StudentFinalGrade> GetAllStudentFinalGrades()
        {
            throw new NotImplementedException();
        }
    }
}
