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
                //TODO: Replace this with IPeriodData.GetPriodForDate
                var period = GetPriodForClassDay(todayClassDay);
                todayClassDay.Period = period ?? throw new InvalidOperationException($"There is no Period for the current ClassDay date:{todayClassDay.DateTime}");

                /*Not calling context.SaveChanges since this is being used
                 as part of another operation*/
                ClassRoomManagerDbContext.ClassDays.Add(todayClassDay);
            }
            return todayClassDay;
        }
        //TODO: Remove this to use IPeriodData.GetPriodForDate instead
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

        public ICollection<StudentFinalGrade> GetAllStudentFinalGrades(int periodId)
        {
            var activityAssignmentsByGradeByPeriod = ClassRoomManagerDbContext.ActivityAssignments
                .Include(aa => aa.Activity)
                    .ThenInclude(a => a.Period)
                .Include(aa => aa.Student)
                .Where(aa => aa.Activity.Type == ActivityType.Grade && aa.Activity.PeriodId == periodId)
                .AsEnumerable();
            
            var activityAssignmentsGroupedByStudentId = activityAssignmentsByGradeByPeriod
                .GroupBy(aa => aa.StudentId)
                .ToDictionary(aa => aa.Key, aa => aa.AsEnumerable());

            var studentFinalGradeList = new List<StudentFinalGrade>();

            foreach (var studentId in activityAssignmentsGroupedByStudentId.Keys)
            {
                var studentClassDaysByStudentByPeriod = ClassRoomManagerDbContext.StudentClassDays
                    .Include(scd => scd.ClassDay)
                    .Where(scd => scd.StudentId == studentId && scd.ClassDay.PeriodId == periodId);


                var activityAssignments = ClassRoomManagerDbContext.ActivityAssignments
                    .Include(aa => aa.Activity)
                    .Where(aa => aa.StudentId == studentId && aa.Activity.Type == ActivityType.Task && aa.Activity.PeriodId == periodId);

                studentFinalGradeList.Add(new StudentFinalGrade
                {
                    StudentId = studentId,
                    FinalGrade = activityAssignmentsGroupedByStudentId[studentId].Sum(aa => aa.Grade * aa.Activity.FinalEvaluationValue) / 10,
                    CreationDate = DateTimeOffset.Now,
                    ModificationDate = DateTimeOffset.Now,
                    PeriodId = periodId,
                    Student = activityAssignmentsGroupedByStudentId[studentId].Select(aa => aa.Student).FirstOrDefault(),
                    AssistanceSummary = $"{studentClassDaysByStudentByPeriod.Where(scd => scd.Assistance == true).Count()}/{studentClassDaysByStudentByPeriod.Count()}",
                    ActivitiesSummary = $"{activityAssignments.Where(aa => aa.Completed).Count()}/{activityAssignments.Count()}"
                });
            }

            return studentFinalGradeList;
        }

        public int AddStudentFinalGrade(IEnumerable<StudentFinalGrade> studentFinalGrades)
        {
            foreach (var studentFinalGrade in studentFinalGrades)
            {
                studentFinalGrade.CreationDate =
                    studentFinalGrade.ModificationDate = DateTimeOffset.Now;
            }
            ClassRoomManagerDbContext.StudentFinalGrades.AddRange(studentFinalGrades);
            return ClassRoomManagerDbContext.SaveChanges();
        }
    }
}
