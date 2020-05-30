using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassRoomManager.Models;
using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClassRoomManager.Controllers
{
    public class HomeController : Controller
    {
        public IClassRoomManagerData ClassRoomManagerData { get; }
        public HomeController(IClassRoomManagerData classRoomManagerData)
        {
            ClassRoomManagerData = classRoomManagerData;
        }

        public IActionResult Home()
        {
            return Json(ClassRoomManagerData.GetAllGroups());
        }

        [Route("group/{groupId:int}")]
        public IActionResult List(int groupId)
        {
            return View(new ListViewModel 
            {
                Students = ClassRoomManagerData.GetStudentsByGroupId(groupId),
                Teams = ClassRoomManagerData.GetTeamsByGroupId(groupId)
            });
        }
        [Route("group/{groupId:int}")]
        [HttpPost]
        public IActionResult List(Team team, int groupId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClassRoomManagerData.AddTeam(team);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("error", ex.Message);
                }
                
            }
            return View(new ListViewModel
            {
                Students = ClassRoomManagerData.GetStudentsByGroupId(groupId),
                Teams = ClassRoomManagerData.GetTeamsByGroupId(groupId)
            });
        }

        public IActionResult Notes()
        {

            return View();
        }
    }
}