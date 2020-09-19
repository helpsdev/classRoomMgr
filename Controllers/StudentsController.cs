using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRoomManager.Models;
using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClassRoomManager.Controllers
{
    public class StudentsController : Controller
    {
        public IStudentData StudentData { get; }
        public StudentsController(IStudentData studentData)
        {
            StudentData = studentData;
        }
        [Route("{studentId:int}")]
        public IActionResult Details(int studentId)
        {
            var studentClassDayList = StudentData.GetStudentClassDaysByStudentId(studentId);

            return View(new StudentDetailsViewModel
            {
                Student = StudentData.GetStudentById(studentId),
                TotalClasses = studentClassDayList.Count,
                ClassesTaken = studentClassDayList.Where(scd => scd.Assistance == true).Count()
            });
        }

        public IActionResult FinalGrades(int? periodId, int? groupId)
        {
            return View(StudentData.GetAllStudentFinalGrades(1));
        }
    }
}
